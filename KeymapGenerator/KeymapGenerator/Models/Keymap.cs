using System.Text.RegularExpressions;
using System.Windows.Controls;
using KeymapGenerator.Data;

namespace KeymapGenerator.Models
{
    public class Keymap
    {
        public Action Action { get; set; }

        private string _keypress;
        public string Keypress
        {
            get {return _keypress;}
            set
            {
                _keypress = value;

                var regex = new Regex(@"(?<=\().+?(?=\))");
                if (regex.IsMatch(_keypress))
                {
                    var keypress = regex.Match(_keypress).Value;
                    DisplayText = TmkKeymappings.GetDisplayText(keypress)[1] == string.Empty
                        ? _keypress
                        : TmkKeymappings.GetDisplayText(keypress)[1];
                    return;
                }

                DisplayText = TmkKeymappings.GetDisplayText(_keypress)[0];
            }
        }

        private string _displayText;

        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                _displayText = value;
                if (Button != null)
                    Button.Content = _displayText;
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
