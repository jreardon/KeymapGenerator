using System.Collections.Generic;
using System.IO;

namespace KeymapGenerator.Infrastructure
{
    public class KeymapFileHelper
    {
        public static List<KeymapLayer> Read(string file)
        {
            var keymapLayers = new List<KeymapLayer>();

            var fileLines = File.ReadAllLines(file);
            if (fileLines.Length == 0) return null;

            var startRead = false;
            KeymapLayer currentLayer;
            foreach (var fileLine in fileLines)
            {
                var line = fileLine.Trim();

                // this is the bit containing the keymaps
                if (line == "const uint16_t PROGMEM keymaps[][MATRIX_ROWS][MATRIX_COLS] = {")
                {
                    startRead = true;
                    continue;
                }

                if (!startRead) continue;

                // we've reached the end of keymap definition block
                if (line == "};") break;

                if (line[0] == '[')
                    currentLayer = new KeymapLayer {LayerNumber = line[1]};
            }
        }
    }
}
