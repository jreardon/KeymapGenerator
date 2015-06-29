using System;
using System.Linq;
using System.Windows;

namespace KeymapGenerator
{
    /// <summary>
    /// Interaction logic for KeymapDialog.xaml
    /// </summary>
    public partial class KeymapDialog : Window
    {
        public KeymapDialog()
        {
            InitializeComponent();

            KeymapTypeCombo.ItemsSource = Enum.GetValues(typeof(KeymapType)).Cast<KeymapType>();
        }

        public string AssociatedLayerText
        {
            get { return AssociatedLayer.Text; }
            set { AssociatedLayer.Text = value; }
        }

        public string KeymapText
        {
            get { return KeyPress.Text; }
            set { KeyPress.Text = value; }
        }

        public KeymapType KeymapTypeSelected
        {
            get { return (KeymapType)KeymapTypeCombo.SelectedItem; }
            set { KeymapTypeCombo.SelectedItem = value; }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
