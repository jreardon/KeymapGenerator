using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using KeymapGenerator.Annotations;
using KeymapGenerator.DataTypes;
using KeymapGenerator.Models;
using KeymapGenerator.Utilities;

namespace KeymapGenerator.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public LayerAction LayerAction { get; set; }
        public ObservableCollection<MenuItem> AvailableLayersMenu { get; set; }
        public List<string> KeymapTypes { get; set; }
        public ObservableCollection<string> AvailableRefLayers { get; set; } 

        private string _selectedRefLayer;
        public string SelectedRefLayer
        {
            get { return _selectedRefLayer; }
            set
            {
                if (value == _selectedRefLayer) return;

                _selectedRefLayer = value;
                OnPropertyChanged();
            }
        }

        private string _selectedKeymapType;
        public string SelectedKeymapType
        {
            get { return _selectedKeymapType; }
            set
            {
                if (value == _selectedKeymapType) return;

                _selectedKeymapType = value;
                OnPropertyChanged();
            }
        }

        private string _keymapText;
        public string KeymapText
        {
            get {return _keymapText; }
            set
            {
                if (value == _keymapText) return;

                _keymapText = value;
                OnPropertyChanged();
            }
        }

        private readonly KeymapLayerController _keymapLayerController;
        private List<KeymapLayer> _keymapLayers;
        private KeymapLayer _currentKeymapLayer;
        private Keymap _selectedKeymap;
        private string _workingFile;

        private readonly Action<object, RoutedEventArgs> _selectedLayerClick;

        public ViewModel(Action<object, RoutedEventArgs> selectedLayerClick)
        {
            _keymapLayers = new List<KeymapLayer>();
            _keymapLayerController = new KeymapLayerController();
            AvailableRefLayers = new ObservableCollection<string>();
            AvailableLayersMenu = new ObservableCollection<MenuItem>();

            var keymapTypes = Enum.GetNames(typeof(KeymapType)).ToList();
            KeymapTypes = keymapTypes;

            _selectedLayerClick = selectedLayerClick;
        }

        public void ImportKeymapFile(string file)
        {
            _workingFile = file;
            var keymapFileReader = new KeymapFileReader();
            _keymapLayers = keymapFileReader.ParseLayers(file);

            foreach (var layer in _keymapLayers)
            {
                var menuItem = new MenuItem {Header = layer.LayerName};
                menuItem.Click += new RoutedEventHandler(_selectedLayerClick);
                AvailableLayersMenu.Add(menuItem);
                _keymapLayerController.PopulateKeymapLayer(layer);
            }
        }

        public void SaveKeymapToFile()
        {
            var keymapFileWriter = new KeymapFileWriter();
            keymapFileWriter.Write(_keymapLayers, _workingFile);
        }

        public KeymapLayer GetKeymapLayer(string layerName)
        {
            _currentKeymapLayer = _keymapLayers.FirstOrDefault(kl => kl.LayerName == layerName);

            if (_currentKeymapLayer == null)
            {
                MessageBox.Show(string.Format("No keymap layer bound to Layer Name '{0}'", layerName));
                return null;
            }

            var keymaps = _currentKeymapLayer.Keymaps;
            for (var i = 0; i < keymaps.GetLength(0); i++)
            {
                for (var j = 0; j < keymaps.GetLength(1); j++)
                {
                    keymaps[i, j].Button.Click += new RoutedEventHandler(KeymapButton_Click());
                }
            }

            return _currentKeymapLayer;
        }

        public KeymapType GetCurrentKeymapType()
        {
            return _selectedKeymap == null ? KeymapType.Keypress : _selectedKeymap.Action.Type;
        }

        public void TriggerLayerAction(string layerName)
        {
            switch (LayerAction)
            {
                case LayerAction.Add:
                    AddLayer(layerName);
                    break;
                case LayerAction.Delete:
                    DeleteLayer(layerName);
                    break;
                case LayerAction.Rename:
                    break;
            }
        }

        private void AddLayer(string layerName)
        {
            layerName = layerName.Trim();
            if (string.IsNullOrEmpty(layerName)) {
                MessageBox.Show("Invalid input");
                return;
            }

            if (_keymapLayers.Any(layer => layer.LayerName == layerName)) {
                MessageBox.Show(string.Format("A layer with the name '{0}' already exists.", layerName));
                return;
            }

            var keymapLayer = _keymapLayerController.GetNewLayer(4, 12, layerName);
            _keymapLayerController.PopulateKeymapLayer(keymapLayer);

            var menuItem = new MenuItem { Header = layerName };
            menuItem.Click += new RoutedEventHandler(_selectedLayerClick);
            AvailableLayersMenu.Add(menuItem);

            _keymapLayers.Add(keymapLayer);
        }

        private void DeleteLayer(string layerName)
        {
            layerName = layerName.Trim();
            var layer = _keymapLayers.FirstOrDefault(x => x.LayerName == layerName);
            if (layer == null)
            {
                MessageBox.Show(string.Format("Error: No layer found by the name '{0}'", layerName));
                return;
            }

            var menuItem = AvailableLayersMenu.First(x => (string) x.Header == layerName);
            AvailableLayersMenu.Remove(menuItem);
            _keymapLayers.Remove(layer);
        }

        public void UpdateKeymapType()
        {
            if (_selectedKeymap == null) return;

            // prevent this from occurring on startup
            if (!string.IsNullOrEmpty(SelectedKeymapType)) 
            {
                var selectedKeymapType = (KeymapType) Enum.Parse(typeof (KeymapType), SelectedKeymapType);
                if (_selectedKeymap.Action.Type != selectedKeymapType) _selectedKeymap.Action.Type = selectedKeymapType;
            }
        }

        public void UpdatedKeymapText()
        {
            if (_selectedKeymap == null) return;

            _selectedKeymap.Keypress = KeymapText;
        }

        public void SetRefLayer()
        {
            if (_selectedKeymap == null || string.IsNullOrEmpty(SelectedRefLayer)) return;

            _selectedKeymap.Action.ReferenceLayer = SelectedRefLayer;

            var referenceLayer = _keymapLayers.First(k => k.LayerName == SelectedRefLayer);
            _selectedKeymap.Action.RefLayerNumber = referenceLayer.LayerNumber;
        }

        public void SetAvailableRefLayers()
        {
            AvailableRefLayers.Clear();
            var availableRefLayers =
                _keymapLayers.Select(x => x.LayerName);

            foreach (var refLayer in availableRefLayers)
                AvailableRefLayers.Add(refLayer);
        }

        public void ResetKeymap()
        {
            _selectedKeymap = null;
            SelectedRefLayer = null;
            SelectedKeymapType = null;
            KeymapText = string.Empty;
        }

        private Action<object, RoutedEventArgs> KeymapButton_Click()
        {
            return (sender, e) =>
            {
                var button = (Button) sender;
                var row = Grid.GetRow(button);
                var col = Grid.GetColumn(button);
                _selectedKeymap = _currentKeymapLayer.Keymaps[row, col];
                
                // reset between key changes
                _selectedKeymapType = null; 
                _selectedRefLayer = null;

                KeymapText = _selectedKeymap.Keypress;
                var selectType = _selectedKeymap.Action.Type.ToString();
                SelectedKeymapType = selectType;
                SelectedRefLayer = _selectedKeymap.Action.ReferenceLayer;
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
