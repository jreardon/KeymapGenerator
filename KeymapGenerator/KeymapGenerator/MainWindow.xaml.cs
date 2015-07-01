using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using KeymapGenerator.Infrastructure;

namespace KeymapGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ComboBoxItem> CbItems { get; set; }
        public ComboBoxItem SelectedCbItem { get; set; }

        private string _addLayerName;
        public string AddLayerName
        {
            get { return _addLayerName; }
            set { _addLayerName = value.Trim(); }
        }

        private readonly KeymapGridController _keymapGridController;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var keymapFileReader = new KeymapFileReader();
            var keymapLayers = keymapFileReader.Read(@"C:\dev\tmk_keyboard\keyboard\planck\extended_keymaps\extended_keymap_default.c");

            _keymapGridController = new KeymapGridController();
            //_keymapGridController.AddLayer(4, 12, "Main");
            foreach (var layer in keymapLayers) _keymapGridController.AddLayer(layer);


            CbItems = new ObservableCollection<ComboBoxItem>();
            var cbItem = new ComboBoxItem { Content = "<--Select-->" };
            SelectedCbItem = cbItem;
            CbItems.Add(cbItem);
            foreach (var layerName in _keymapGridController.GetLayerNames()) {
                CbItems.Add(new ComboBoxItem { Content = layerName });
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var keymapLayer =
                _keymapGridController.GetLayer(SelectedCbItem.Content.ToString());

            KeymapContainer.Children.Clear();
            if (keymapLayer == null) return;

            _keymapGridController.ChangeKeymapLayer(keymapLayer);
            KeymapContainer.Children.Add(keymapLayer.KeymapGrid);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_addLayerName)) {
                MessageBox.Show("Invalid input");
                return;
            }

            var existingLayerName = _keymapGridController.GetLayer(_addLayerName);
            if (existingLayerName != null) {
                MessageBox.Show(string.Format("A layer with the name '{0}' already exists.", _addLayerName));
                return;
            }

            _keymapGridController.AddLayer(4, 12, _addLayerName);
            CbItems.Add(new ComboBoxItem { Content = _addLayerName });

            TxtAddLayer.Clear();
        }
    }
}
