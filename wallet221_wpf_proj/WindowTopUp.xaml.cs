using System.Windows;

namespace wallet221_wpf_proj
{
    /// <summary>
    /// Interaction logic for WindowTopUp.xaml
    /// </summary>
    public partial class WindowTopUp : Window
    {
        public WindowTopUp()
        {
            InitializeComponent();
        }

        private void topUpBtn(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
