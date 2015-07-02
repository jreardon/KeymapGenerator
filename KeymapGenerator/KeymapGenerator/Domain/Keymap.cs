namespace KeymapGenerator.Domain
{
    public class Keymap
    {
        public KeymapType Type { get; set; }
        public string Action { get; set; }
        public string Text { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

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
