using System.Collections.Generic;
using System.IO;
using System.Linq;
using KeymapGenerator.DataTypes;
using KeymapGenerator.Models;

namespace KeymapGenerator.Utilities
{
    public class KeymapFileWriter
    {
        public void Write(List<KeymapLayer> keymapLayers, string file)
        {
            var fileLines = new List<string>
            {
                "#include \"extended_keymap_common.h\"",
                "",
                "const uint16_t PROGMEM keymaps[][MATRIX_ROWS][MATRIX_COLS] = {"
            };

            foreach (var layer in keymapLayers)
            {
                fileLines.Add(string.Format("[{0}] = {{ /* {1} */", layer.LayerNumber, layer.LayerName));

                for (var row = 0; row < layer.NumberRows; row++)
                {
                    var line = "{";
                    for (var col = 0; col < layer.NumberCols; col++)
                    {
                        var keymap = layer.Keymaps[row, col];
                        line += keymap.Keypress;

                        if (col != layer.NumberCols) line += ",";
                    }
                    line += "}";

                    if (row != layer.NumberRows) line += ",";

                    fileLines.Add(line);
                }

                fileLines.Add(layer.LayerNumber == keymapLayers.Last().LayerNumber ? "}" : "},");
            }

            fileLines.Add("};");
            fileLines.Add("");
            fileLines.Add("");

            // add Actions
            fileLines.Add("const uint16_t PROGMEM fn_actions[] = {");
            const string actionLine = "[{0}] = {1}({2}),";
            fileLines.AddRange(from action in GetActions(keymapLayers)
                let actionMethod =
                    action.Type == KeymapType.MomentaryLayer ? "ACTION_LAYER_MOMENTARY" : "ACTION_DEFAULT_LAYER_SET"
                select string.Format(actionLine, action.Number, actionMethod, action.RefLayerNumber));
            fileLines.Add("};");

            fileLines.AddRange(GetMacroLines());

            File.WriteAllLines(file, fileLines);
        }
        
        private static IEnumerable<Action> GetActions(IEnumerable<KeymapLayer> keymapLayers)
        {
            var actions = new List<Action>();

            foreach (var layer in keymapLayers)
            {
                foreach (var keymap in layer.Keymaps)
                {
                    if (keymap.Action.Type == KeymapType.Keypress || actions.Any(a => a.Number == keymap.Action.Number))
                        continue;

                    actions.Add(keymap.Action);
                }
            }

            return actions;
        }

        private static IEnumerable<string> GetMacroLines()
        {
            return new List<string>
            {
                "",
                "const macro_t *action_get_macro(keyrecord_t *record, uint8_t id, uint8_t opt) ",
                "{",
                "  // MACRODOWN only works in this function",
                "    switch(id) {",
                "      case 0:",
                "        return MACRODOWN(T(CM_T), END);",
                "      break;",
                "    }",
                "    return MACRO_NONE;",
                "};"
            };
        }
    }
}
