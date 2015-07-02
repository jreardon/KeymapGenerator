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
        public IEnumerable<KeymapType> KeymapTypes { get; set; }
        public ComboBox SelectedKaymapType { get; set; }
        public string KeymapText { get; set; }

        private string _addLayerName;
        public string AddLayerName
        {
            get { return _addLayerName; }
            set { _addLayerName = value.Trim(); }
        }

        private readonly KeymapGrid _keymapGridController;

        public ViewModel()
        {
            var keymapFileReader = new KeymapFileReader();
            var keymapLayers = keymapFileReader.Read(@"C:\dev\tmk_keyboard\keyboard\planck\extended_keymaps\extended_keymap_default.c");

            _keymapGridController = new KeymapGrid();
            foreach (var layer in keymapLayers) _keymapGridController.AddLayer(layer);

            AvailableLayers = new ObservableCollection<ComboBoxItem>();
            var cbItem = new ComboBoxItem { Content = "<--Select-->" };
            SelectedLayer = cbItem;
            AvailableLayers.Add(cbItem);
            foreach (var layerName in _keymapGridController.GetLayerNames()) {
                AvailableLayers.Add(new ComboBoxItem { Content = layerName });
            }

            KeymapTypes = Enum.GetValues(typeof(KeymapType)).Cast<KeymapType>();
        }

        public KeymapLayer GetKeymapLayer()
        {
            var keymapLayer =
                _keymapGridController.GetLayer(SelectedLayer.Content.ToString());

            _keymapGridController.ChangeKeymapLayer(keymapLayer);

            return keymapLayer;
        }

        public void AddLayer()
        {
            if (string.IsNullOrEmpty(_addLayerName)) {
                MessageBox.Show("Invalid input");
                return;
            }

            var existingLayerName = _keymapGridController.GetLayer(_addLayerName);
            if (existingLayerName != null) {
                MessageBox.Show(string.Format("A layer with the name '{0}' already exists.", _addLayerName));
                return;
            }

            _keymapGridController.AddLayer(4, 12, _addLayerName);
            AvailableLayers.Add(new ComboBoxItem { Content = _addLayerName });
        }
    }
}
