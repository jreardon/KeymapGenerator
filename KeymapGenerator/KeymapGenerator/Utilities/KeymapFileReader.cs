using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KeymapGenerator.DataTypes;
using KeymapGenerator.Models;
using Action = KeymapGenerator.Models.Action;

namespace KeymapGenerator.Utilities
{
    public class KeymapFileReader
    {
        private int _currentRowCount;
        private int _currentColumnCount;
        private int _currentLayerNumber;
        private string _currentLayerName;
        private List<List<string>> _currentRowCollection = new List<List<string>>();

        public List<KeymapLayer> ParseLayers(string file)
        {
            var keymapLayers = new List<KeymapLayer>();

            var fileLines = File.ReadAllLines(file);
            if (fileLines.Length == 0) return null;

            var startRead = false;
            foreach (var line in fileLines.Select(fileLine => fileLine.Trim()))
            {
                // this is the bit containing the keymaps
                if (line == "const uint16_t PROGMEM keymaps[][MATRIX_ROWS][MATRIX_COLS] = {")
                {
                    startRead = true;
                    continue;
                }

                if (!startRead) continue;

                // we've reached the end of keymap definition block
                if (line == "};") break;

                // start new layer
                if (line[0] == '[')
                {
                    var layerNumber = line[1].ToString();
                    _currentLayerNumber = Convert.ToInt32(layerNumber);
                    _currentLayerName = GetLayerName(line);
                    continue;
                }

                // start new row
                if (line[0] == '{')
                {
                    _currentRowCount++;
                    var row = line.Replace("{", "").Replace("}", "").Split(',').Where(x => x != "").ToList();
                    _currentColumnCount = row.Count();
                    _currentRowCollection.Add(row);
                    continue;
                }

                if (line != "}" && line != "},") continue;

                // reached the end of the current KeymapLayer
                var currentKeymapLayer = new KeymapLayer(_currentRowCount, _currentColumnCount)
                {
                    LayerNumber = _currentLayerNumber,
                    LayerName = _currentLayerName
                };

                ConvertRowCollectionToKeymapLayer(currentKeymapLayer);
                keymapLayers.Add(currentKeymapLayer);

                // reset
                _currentRowCount = 0;
                _currentColumnCount = 0;
                _currentRowCollection = new List<List<string>>();
                _currentLayerNumber = -1;
                _currentLayerName = string.Empty;
            }

            var actions = ParseActions(fileLines);
            AssignActions(actions, keymapLayers);

            return keymapLayers;
        }

        private void ConvertRowCollectionToKeymapLayer(KeymapLayer keymapLayer)
        {
            for (var rowNum = 0; rowNum < _currentRowCollection.Count; rowNum++)
            {
                var row = _currentRowCollection[rowNum];
                for (var colNum = 0; colNum < row.Count; colNum++)
                {
                    keymapLayer.Keymaps[rowNum, colNum] = new Keymap
                    {
                        Row = rowNum,
                        Col = colNum,
                        Text = row[colNum].Trim()
                    };
                }
            }
        }

        private static string GetLayerName(string line)
        {
            var startRead = false;
            var layerName = "";

            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '/' && line[i + 1] == '*')
                {
                    i++;
                    startRead = true;
                    continue;
                }

                if (!startRead) continue;

                if (line[i] == '*' && line[i + 1] == '/') break;

                layerName += line[i];
            }

            return layerName.Trim();
        }

        private static void AssignActions(IEnumerable<Action> actions, IEnumerable<KeymapLayer> keymapLayers)
        {
            var actionList = actions.ToList();
            var layerList = keymapLayers.ToList();

            foreach (var layer in layerList)
            {
                foreach (var keymap in layer.Keymaps)
                {
                    if (!keymap.Text.Contains("FUNC")) continue;

                    var actionNumber = ExtractLayerNumber(keymap.Text);
                    keymap.Action = actionList.First(a => a.Number == actionNumber);
                    keymap.Action.ReferenceLayer =
                        layerList.First(k => k.LayerNumber == keymap.Action.RefLayerNumber).LayerName;
                }
            }
        }

        private static IEnumerable<Action> ParseActions(IEnumerable<string> fileLines)
        {
            var startRead = false;
            foreach (var line in fileLines.Select(l => l.Trim()).Where(line => line.Length != 0))
            {
                if (line == "const uint16_t PROGMEM fn_actions[] = {")
                {
                    startRead = true;
                    continue;
                }

                if (!startRead) continue;

                // reached the end
                if (line == "};") break;

                if (line[0] != '[') continue;

                var actionNumber = Convert.ToInt32(line[1].ToString());
                var layerNumber = ExtractLayerNumber(line);
                var actionType = KeymapType.Keypress;

                if (line.Contains("ACTION_LAYER_MOMENTARY"))
                    actionType = KeymapType.MomentaryLayer;

                if (line.Contains("ACTION_DEFAULT_LAYER_SET"))
                    actionType = KeymapType.SetLayer;

                yield return new Action
                {
                    Number = actionNumber,
                    Type = actionType,
                    RefLayerNumber = layerNumber
                };
            }
        }

        private static int ExtractLayerNumber(string line)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] != '(') continue;

                return Convert.ToInt32(line[i + 1].ToString());
            }

            return -1;
        }
    }
}
