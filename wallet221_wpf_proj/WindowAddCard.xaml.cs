using System.Windows;

namespace wallet221_wpf_proj
{
    /// <summary>
    /// Interaction logic for WindowAddCard.xaml
    /// </summary>
    public partial class WindowAddCard : Window
    {
        public WindowAddCard()
        {
            InitializeComponent();
        }

        private void addBtn(object sender, RoutedEventArgs e)
        {
            this.DialogResult=true;
        }
    }
}
