using System.Windows.Controls;

namespace KeymapGenerator.Models
{
    public class KeymapGrid
    {
        public static Grid GetNewKeymapGrid(int numRows, int numCols, Keymap[,] keymaps)
        {
            var keymapGrid = new Grid();
            keymapGrid.Children.Clear();

            var rows = new RowDefinition[numRows];
            var columns = new ColumnDefinition[numCols];

            for (var i = 0; i < numRows; i++)
            {
                rows[i] = new RowDefinition();
                keymapGrid.RowDefinitions.Add(rows[i]);
            }

            for (var i = 0; i < numCols; i++)
            {
                columns[i] = new ColumnDefinition();
                keymapGrid.ColumnDefinitions.Add(columns[i]);
            }

            for (var i = 0; i < numRows; i++)
            {
                for (var j = 0; j < numCols; j++)
                {
                    var button = new Button();
                    keymaps[i, j].Button = button;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    keymapGrid.Children.Add(button);
                }
            }

            return keymapGrid;
        }

        //private Action<object, RoutedEventArgs> KeymapButton_Click()
        //{
        //    return (sender, e) => {
        //        var keymapButton = (Button)sender;
        //        var row = Grid.GetRow(keymapButton);
        //        var col = Grid.GetColumn(keymapButton);
        //        var keymap = _currentKeymapLayer.Keymaps[row, col];

        //        try {
        //            var keymapDialog = new KeymapDialog();
        //            if (keymapDialog.ShowDialog() == true) 
        //            {
        //                keymap.Text = keymapDialog.KeymapText;
        //                keymap.Action = keymapDialog.AssociatedLayerText;
        //                keymap.Type = keymapDialog.KeymapTypeSelected;
        //                keymapButton.Content = keymap.Text;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            // ignored
        //        }
        //    };
        //}
    }
}
