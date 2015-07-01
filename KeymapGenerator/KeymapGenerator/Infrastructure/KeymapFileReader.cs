using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KeymapGenerator.Infrastructure
{
    public class KeymapFileReader
    {
        private int _currentRowCount;
        private int _currentColumnCount;
        private int _currentLayerNumber;
        private List<List<string>> _currentRowCollection = new List<List<string>>();

        public List<KeymapLayer> Read(string file)
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

                if (line[0] == '[')
                {
                    var layerNumber = line[1].ToString();
                    _currentLayerNumber = Convert.ToInt32(layerNumber);
                    continue;
                }

                if (line[0] == '{')
                {
                    _currentRowCount++;
                    var row = line.Replace("{", "").Replace("}", "").Split(',').Where(x => x != "").ToList();
                    _currentColumnCount = row.Count();
                    _currentRowCollection.Add(row);
                    continue;
                }

                if (line != "}" && line != "},") continue;

                var currentKeymapLayer = new KeymapLayer(_currentRowCount, _currentColumnCount)
                {
                    LayerNumber = _currentLayerNumber
                };

                ConvertRowCollectionToKeymapLayer(currentKeymapLayer);
                keymapLayers.Add(currentKeymapLayer);

                _currentRowCount = 0;
                _currentColumnCount = 0;
                _currentRowCollection = new List<List<string>>();
                _currentLayerNumber = -1;
            }

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
                        Text = row[colNum].Trim(), 
                        Value = row[colNum].Trim()
                    };
                }
            }
        }
    }
}
