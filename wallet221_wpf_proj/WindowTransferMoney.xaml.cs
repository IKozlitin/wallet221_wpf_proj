
using System.Windows;


namespace wallet221_wpf_proj
{
    /// <summary>
    /// Interaction logic for WindowTransferMoney.xaml
    /// </summary>
    public partial class WindowTransferMoney : Window
    {
        public WindowTransferMoney()
        {
            InitializeComponent();            
        }              

        private void transferBtn(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
