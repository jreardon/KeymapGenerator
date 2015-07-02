using System.Windows.Controls;

namespace KeymapGenerator.Domain
{
    public class KeymapLayer
    {
        private string _layerName;

        public int LayerNumber { get; set; }

        public string LayerName
        {
            get { return _layerName ?? LayerNumber.ToString(); }
            set { _layerName = value; }
        }
        public int NumberRows { get; set; }
        public int NumberCols { get; set; }
        public Keymap[,] Keymaps { get; set; }
        public Button[,] Buttons { get; set; }
        public Grid KeymapGrid { get; set; }

        public KeymapLayer(int numRows, int numCols)
        {
            NumberRows = numRows;
            NumberCols = numCols;
            Keymaps = new Keymap[numRows, numCols];
        }
    }
}