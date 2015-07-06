using KeymapGenerator.DataTypes;

namespace KeymapGenerator.Models
{
    public class Action
    {
        public KeymapType ActionType { get; set; }
        public int ActionNumber { get; set; }
        public int RefLayerNumber { get; set; }
    }
}
