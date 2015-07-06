namespace KeymapGenerator.Models
{
    public class KeymapLayerController
    {
        private int _currentLayerNumber;

        public KeymapLayer GetNewLayer(int numRows, int numCols, string name)
        {
            var keymaps = GetNewKeymapsArray(numRows, numCols);
            var keymapGrid = KeymapGrid.GetNewKeymapGrid(numRows, numCols, keymaps);
            return new KeymapLayer(numRows, numCols)
            {
                LayerName = name,
                LayerNumber = _currentLayerNumber++,
                KeymapGrid = keymapGrid,
                Keymaps = GetNewKeymapsArray(numRows, numCols)
            };
        }

        public void PopulateKeymapLayer(KeymapLayer keymapLayer)
        {
            keymapLayer.KeymapGrid = KeymapGrid.GetNewKeymapGrid(keymapLayer.NumberRows, keymapLayer.NumberCols, keymapLayer.Keymaps);

            for (var i = 0; i < keymapLayer.NumberRows; i++) {
                for (var j = 0; j < keymapLayer.NumberCols; j++) {
                    keymapLayer.Keymaps[i, j].Button.Content = keymapLayer.Keymaps[i, j].Text;
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
