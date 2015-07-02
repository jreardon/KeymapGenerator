using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KeymapGenerator.DataTypes;
using KeymapGenerator.Models;
using KeymapGenerator.Utilities;

namespace KeymapGenerator.ViewModels
{
    public class ViewModel
    {
        public ObservableCollection<ComboBoxItem> AvailableLayers { get; set; }
        public ComboBoxItem SelectedLayer { get; set; }
        public List<KeymapType> KeymapTypes { get; set; }
        public KeymapType SelectedKaymapType { get; set; }
        public string KeymapText { get; set; }

        private string _addLayerName;
        public string AddLayerName
        {
            get { return _addLayerName; }
            set { _addLayerName = value.Trim(); }
        }

        private readonly KeymapLayerController _keymapLayerController;
        private readonly List<KeymapLayer> _keymapLayers;
        private KeymapLayer _currentKeymapLayer;

        public ViewModel()
        {
            _keymapLayers = new List<KeymapLayer>();
            var keymapFileReader = new KeymapFileReader();
            _keymapLayers = keymapFileReader.Read(@"C:\dev\tmk_keyboard\keyboard\planck\extended_keymaps\extended_keymap_default.c");

            _keymapLayerController = new KeymapLayerController();
            foreach (var layer in _keymapLayers) _keymapLayerController.PopulateKeymapLayer(layer);

            AvailableLayers = new ObservableCollection<ComboBoxItem>();
            var cbItem = new ComboBoxItem { Content = "<--Select-->" };
            SelectedLayer = cbItem;
            AvailableLayers.Add(cbItem);
            foreach (var layerName in _keymapLayers.Select(kl => kl.LayerName)) {
                AvailableLayers.Add(new ComboBoxItem { Content = layerName });
            }

            var keymapTypes = Enum.GetValues(typeof(KeymapType)).Cast<KeymapType>().ToList();
            KeymapTypes = keymapTypes;
        }

        public KeymapLayer GetKeymapLayer()
        {
            _currentKeymapLayer = _keymapLayers.FirstOrDefault(kl => kl.LayerName == SelectedLayer.Content.ToString());
            return _currentKeymapLayer;
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
            for (var i = 0; i < keymapLayer.Buttons.GetLength(0); i++) {
                for (var j = 0; j < keymapLayer.Buttons.GetLength(1); j++) {
                    keymapLayer.Buttons[i, j].Click += new RoutedEventHandler(KeymapButton_Click());
                }
            }
            _keymapLayers.Add(keymapLayer);

            AvailableLayers.Add(new ComboBoxItem { Content = _addLayerName });
        }

        private Action<object, RoutedEventArgs> KeymapButton_Click()
        {
            return (sender, e) =>
            {
                var keymapButton = (Button) sender;
                var row = Grid.GetRow(keymapButton);
                var col = Grid.GetColumn(keymapButton);
                var keymap = _currentKeymapLayer.Keymaps[row, col];

                KeymapText = keymap.Text;
                SelectedKaymapType = keymap.Type;
            };
        }
    }
}
