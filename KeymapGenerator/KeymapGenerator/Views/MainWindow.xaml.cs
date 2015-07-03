using System.Windows;
using System.Windows.Controls;
using KeymapGenerator.ViewModels;

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
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var keymapLayer = _viewModel.GetKeymapLayer();

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
            _viewModel.UpdateSelectedButton();
        }

        private void CbKeymapType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.UpdateSelectedButton();
        }
    }
}
