using System.Windows.Controls;

namespace KeymapGenerator
{
    public class KeymapLayer
    {
        public string LayerName { get; set; }
        public int NumberRows { get; set; }
        public int NumberCols { get; set; }
        public Keymap[,] Keymaps { get; set; }
        public Grid KeymapGrid { get; set; }

        public KeymapLayer(int numRows, int numCols)
        {
            NumberRows = numRows;
            NumberCols = numCols;
            Keymaps = new Keymap[numRows, numCols];
        }
    }
}