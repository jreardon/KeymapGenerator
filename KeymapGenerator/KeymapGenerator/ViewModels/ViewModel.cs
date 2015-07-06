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
        public ObservableCollection<ComboBoxItem> AvailableLayers { get; set; }
        public ComboBoxItem SelectedLayer { get; set; }
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

        private string _addLayerName;
        public string AddLayerName
        {
            get { return _addLayerName; }
            set { _addLayerName = value.Trim(); }
        }

        private readonly KeymapLayerController _keymapLayerController;
        private List<KeymapLayer> _keymapLayers;
        private KeymapLayer _currentKeymapLayer;
        private Keymap _selectedKeymap;

        public ViewModel()
        {
            _keymapLayers = new List<KeymapLayer>();
            _keymapLayerController = new KeymapLayerController();
            AvailableLayers = new ObservableCollection<ComboBoxItem>();
            AvailableRefLayers = new ObservableCollection<string>();

            var cbItem = new ComboBoxItem { Content = "<--Select-->" };
            SelectedLayer = cbItem;
            AvailableLayers.Add(cbItem);

            var keymapTypes = Enum.GetNames(typeof(KeymapType)).ToList();
            KeymapTypes = keymapTypes;
        }

        public void ImportKeymapFile(string file)
        {
            var keymapFileReader = new KeymapFileReader();
            _keymapLayers = keymapFileReader.Read(file);

            foreach (var layer in _keymapLayers) {
                AvailableLayers.Add(new ComboBoxItem { Content = layer.LayerName });
                _keymapLayerController.PopulateKeymapLayer(layer);
            }
        }

        public KeymapLayer GetKeymapLayer()
        {
            _currentKeymapLayer = _keymapLayers.FirstOrDefault(kl => kl.LayerName == SelectedLayer.Content.ToString());

            if (_currentKeymapLayer != null)
            {
                var keymaps = _currentKeymapLayer.Keymaps;
                for (var i = 0; i < keymaps.GetLength(0); i++)
                {
                    for (var j = 0; j < keymaps.GetLength(1); j++)
                    {
                        keymaps[i, j].Button.Click += new RoutedEventHandler(KeymapButton_Click());
                    }
                }
            }

            return _currentKeymapLayer;
        }

        public KeymapType GetCurrentKeymapType()
        {
            return _selectedKeymap == null ? KeymapType.Keypress : _selectedKeymap.Type;
        }

        public void AddLayer()
        {
            if (string.IsNullOrEmpty(_addLayerName)) {
                MessageBox.Show("Invalid input");
                return;
            }

            if (_keymapLayers.Any(layer => layer.LayerName == _addLayerName)) {
                MessageBox.Show(string.Format("A layer with the name '{0}' already exists.", _addLayerName));
                return;
            }

            var keymapLayer = _keymapLayerController.GetNewLayer(4, 12, _addLayerName);
            _keymapLayerController.PopulateKeymapLayer(keymapLayer);
            AvailableLayers.Add(new ComboBoxItem { Content = _addLayerName });

            _keymapLayers.Add(keymapLayer);
        }

        public void UpdateKeymapType()
        {
            if (_selectedKeymap == null) return;

            // prevent this from occurring on startup
            if (!string.IsNullOrEmpty(SelectedKeymapType)) 
            {
                var selectedKeymapType = (KeymapType) Enum.Parse(typeof (KeymapType), SelectedKeymapType);
                if (_selectedKeymap.Type != selectedKeymapType) _selectedKeymap.Type = selectedKeymapType;
            }
        }

        public void UpdatedKeymapText()
        {
            if (_selectedKeymap == null) return;

            _selectedKeymap.Text = KeymapText;
        }

        public void SetRefLayer()
        {
            if (_selectedKeymap == null || string.IsNullOrEmpty(SelectedRefLayer)) return;

            var referenceLayer = SelectedRefLayer.Split().Last();

            _selectedKeymap.ReferenceLayer = referenceLayer;
        }

        public void SetAvailableRefLayers()
        {
            AvailableRefLayers.Clear();
            var availableRefLayers =
                _keymapLayers.Where(x => x.LayerName != _currentKeymapLayer.LayerName).Select(x => x.LayerName);

            foreach (var refLayer in availableRefLayers)
                AvailableRefLayers.Add(refLayer);
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

                KeymapText = _selectedKeymap.Text;
                var selectType = _selectedKeymap.Type.ToString();
                SelectedKeymapType = selectType;
                SelectedRefLayer = _selectedKeymap.ReferenceLayer;
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
