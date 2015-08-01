using System;
using System.Windows;
using System.Windows.Controls;
using KeymapGenerator.DataTypes;
using KeymapGenerator.ViewModels;
using Microsoft.Win32;

namespace KeymapGenerator.Views
{
    public partial class MainWindow
    {
        private readonly ViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel(SelectedLayer_Click());
            DataContext = _viewModel;
            CbRefLayer.IsEnabled = false;
        }

        private Action<object, RoutedEventArgs> SelectedLayer_Click()
        {
            return (sender, e) =>
            {
                var menuItem = (MenuItem) sender;
                var keymapLayer = _viewModel.GetKeymapLayer(menuItem.Header.ToString());

                _viewModel.ResetKeymap();
                _viewModel.SetAvailableRefLayers();

                KeymapContainer.Children.Clear();
                if (keymapLayer == null) return;

                KeymapContainer.Children.Add(keymapLayer.KeymapGrid);

                _viewModel.CurrentLayerName = keymapLayer.LayerName;
            };
        }

        private void AddLayer_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LayerAction = LayerAction.Add;
            AddLayerInputBox.Visibility = Visibility.Visible;
        }

        private void AddLayerOk_Click(object sender, RoutedEventArgs e)
        {
            AddLayerInputBox.Visibility = Visibility.Collapsed;

            var layerName = AddLayerTextBox.Text;
            _viewModel.TriggerLayerAction(layerName);

            AddLayerTextBox.Text = string.Empty;
        }

        private void AddLayerCancel_Click(object sender, RoutedEventArgs e)
        {
            AddLayerInputBox.Visibility = Visibility.Collapsed;
            AddLayerTextBox.Text = string.Empty;
        }

        private void DeleteLayer_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteLayer();
            KeymapContainer.Children.Clear();
        }

        private void RenameLayer_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LayerAction = LayerAction.Rename;
            AddLayerInputBox.Visibility = Visibility.Visible;
        }

        private void TxtKeymapText_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.UpdatedKeymapText();
        }

        private void CbKeymapType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.UpdateKeymapType();

            var keymapType = _viewModel.GetCurrentKeymapType();
            if (keymapType == KeymapType.MomentaryLayer || keymapType == KeymapType.SetLayer)
            {
                CbRefLayer.IsEnabled = true;
            }
            else
            {
                _viewModel.SelectedRefLayer = "";
                CbRefLayer.IsEnabled = false;
            }
        }

        private void BtnImportKeymap_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            var dialogSuccessful = dialog.ShowDialog();

            try
            {
                if (dialogSuccessful == true)
                    _viewModel.ImportKeymapFile(dialog.FileName);
                else
                    MessageBox.Show("File selection failed.");
            }
            catch (Exception)
            {
                MessageBox.Show("Import failed.");
                return;
            }

            MessageBox.Show("Succesfully imported keymap");
        }

        private void CbRefLayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SetRefLayer();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.SaveKeymapToFile();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while saving. It is likely that the save failed.");
                return;
            }

            MessageBox.Show("Save successful.");
        }
    }
}
