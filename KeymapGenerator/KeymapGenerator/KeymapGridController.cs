using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KeymapGenerator
{
    public class KeymapGridController
    {
        private KeymapLayer _currentKeymapLayer;
        public List<KeymapLayer> KeymapLayers { get; set; }

        public KeymapGridController()
        {
            KeymapLayers = new List<KeymapLayer>();
        }

        public void ChangeKeymapLayer(KeymapLayer keymapLayer)
        {
            _currentKeymapLayer = GetLayer(keymapLayer.LayerName);
        }

        public void AddLayer(int numRows, int numCols, string name)
        {
            var keymaps = new Keymap[numRows, numCols];
            var keymapGrid = new Grid();
            keymapGrid.Children.Clear();

            var rows = new RowDefinition[numRows];
            var columns = new ColumnDefinition[numCols];

            for (var i = 0; i < numRows; i++) {
                rows[i] = new RowDefinition();
                keymapGrid.RowDefinitions.Add(rows[i]);
            }

            for (var i = 0; i < numCols; i++) {
                columns[i] = new ColumnDefinition();
                keymapGrid.ColumnDefinitions.Add(columns[i]);
            }

            var buttons = new Button[numRows, numCols];
            for (var i = 0; i < numRows; i++) {
                for (var j = 0; j < numCols; j++) {
                    keymaps[i, j] = new Keymap(i, j);
                    var button = new Button();
                    button.Click += new RoutedEventHandler(KeymapButton_Click());
                    buttons[i, j] = button;
                    Grid.SetRow(buttons[i, j], i);
                    Grid.SetColumn(buttons[i, j], j);
                    keymapGrid.Children.Add(buttons[i, j]);
                }
            }

            KeymapLayers.Add(new KeymapLayer(numRows, numCols)
            {
                LayerName = name,
                KeymapGrid = keymapGrid,
                Keymaps = keymaps
            });
        }

        private Action<object, RoutedEventArgs> KeymapButton_Click()
        {
            return (object sender, RoutedEventArgs e) => {
                var keymapButton = (Button)sender;
                var row = Grid.GetRow(keymapButton);
                var col = Grid.GetColumn(keymapButton);
                var keymap = _currentKeymapLayer.Keymaps[row, col];

                try {
                    var keymapDialog = new KeymapDialog();
                    if (keymapDialog.ShowDialog() == true) {
                        keymap.Text = keymapDialog.KeymapText;
                        keymap.Value = keymapDialog.AssociatedLayerText;
                        keymap.Type = keymapDialog.KeymapTypeSelected;
                        keymapButton.Content = keymap.Text;
                    }
                } catch (Exception) { }
            };
        }

        public List<string> GetLayerNames()
        {
            return KeymapLayers.Select(x => x.LayerName).ToList();
        }

        public KeymapLayer GetLayer(string layerName)
        {
            return KeymapLayers.FirstOrDefault(keymapLayer => keymapLayer.LayerName == layerName);
        }
    }
}
