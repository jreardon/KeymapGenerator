using KeymapGenerator.DataTypes;

namespace KeymapGenerator.Models
{
    public class Action
    {
        public KeymapType Type { get; set; }
        public int Number { get; set; }
        public string ReferenceLayer { get; set; }
        public int RefLayerNumber { get; set; }

        public Action()
        {
            Type = KeymapType.Keypress;
        }
    }
}
