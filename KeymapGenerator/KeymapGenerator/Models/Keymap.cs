using System.Windows.Controls;
using KeymapGenerator.DataTypes;

namespace KeymapGenerator.Models
{
    public class Keymap
    {
        public Action Action { get; set; }

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
            Action = new Action();
        }

        public Keymap(int row, int col)
        {
            Row = row;
            Col = col;
            Action = new Action();
        }
    }
}
