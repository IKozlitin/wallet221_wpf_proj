using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace wallet221_wpf_proj
{
    /// <summary>
    /// Interaction logic for WindowWithdrawal.xaml
    /// </summary>
    public partial class WindowWithdrawal : Window
    {
        public WindowWithdrawal()
        {
            InitializeComponent();
        }
        private void withdrawalBtn(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
