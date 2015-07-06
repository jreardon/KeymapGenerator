using System.Windows.Controls;

namespace KeymapGenerator.Models
{
    public class KeymapLayer
    {
        private string _layerName;

        public int LayerNumber { get; set; }

        public string LayerName
        {
            get { return string.IsNullOrEmpty(_layerName) ? LayerNumber.ToString() : _layerName; }
            set { _layerName = value; }
        }
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