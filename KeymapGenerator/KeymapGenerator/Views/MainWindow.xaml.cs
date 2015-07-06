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
            _viewModel = new ViewModel();
            DataContext = _viewModel;
            CbRefLayer.IsEnabled = false;
        }

        private void CbKeymapLayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var keymapLayer = _viewModel.GetKeymapLayer();

            _viewModel.ResetKeymap();
            _viewModel.SetAvailableRefLayers();

            KeymapContainer.Children.Clear();
            if (keymapLayer == null) return;

            KeymapContainer.Children.Add(keymapLayer.KeymapGrid);
        }

        private void AddLayer_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddLayer();
            TxtAddLayer.Clear();
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

            if (dialogSuccessful == true)
                _viewModel.ImportKeymapFile(dialog.FileName);
        }

        private void CbRefLayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SetRefLayer();
        }
    }
}
