using System.Windows.Controls;
using KeymapGenerator.DataTypes;

namespace KeymapGenerator.Models
{
    public class Keymap
    {
        public KeymapType Type { get; set; }
        public int ActionNumber { get; set; }
        public string ReferenceLayer { get; set; }

        private string _text;
        public string Text
        {
            get {return _text;}
            set
            {
                _text = value;
                if (Button != null)
                    Button.Content = _text;
            }
        }
        public int Row { get; set; }
        public int Col { get; set; }
        public Button Button { get; set; }

        public Keymap()
        {

        }

        public Keymap(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
