using System.Windows.Controls;

namespace KeymapGenerator.Models
{
    public class KeymapLayerController
    {
        public KeymapLayer GetNewLayer(int numRows, int numCols, string name)
        {
            Button[,] buttons;
            var keymapGrid = KeymapGrid.GetNewKeymapGrid(numRows, numCols, out buttons);
            return new KeymapLayer(numRows, numCols)
            {
                LayerName = name,
                KeymapGrid = keymapGrid,
                Buttons = buttons,
                Keymaps = GetNewKeymapsArray(numRows, numCols)
            };
        }

        public void PopulateKeymapLayer(KeymapLayer keymapLayer)
        {
            Button[,] buttons;
            keymapLayer.KeymapGrid = KeymapGrid.GetNewKeymapGrid(keymapLayer.NumberRows, keymapLayer.NumberCols, out buttons);
            keymapLayer.Buttons = buttons;

            for (var i = 0; i < keymapLayer.NumberRows; i++) {
                for (var j = 0; j < keymapLayer.NumberCols; j++) {
                    keymapLayer.Buttons[i, j].Content = keymapLayer.Keymaps[i, j].Text;
                }
            }
        }

        private static Keymap[,] GetNewKeymapsArray(int numRows, int numCols)
        {
            var keymaps = new Keymap[numRows, numCols];

            for (var i = 0; i < numRows; i++) {
                for (var j = 0; j < numCols; j++) {
                    keymaps[i, j] = new Keymap(i, j);
                }
            }

            return keymaps;
        }
    }
}
