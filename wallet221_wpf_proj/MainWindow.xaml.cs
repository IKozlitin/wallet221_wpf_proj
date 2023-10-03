using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes; 

namespace wallet221_wpf_proj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WalletDbContext db = new WalletDbContext();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Clients.Load();
            client.DataContext = db.Clients.Local.ToObservableCollection();
            db.RublesCards.Load();
            cardList.DataContext = db.RublesCards.Local.ToObservableCollection();
            db.RublesDeposits.Load();
            depositList.DataContext = db.RublesDeposits.Local.ToObservableCollection();
            db.RateLists.Load();
            rateList.DataContext = db.RateLists.Local.ToObservableCollection();
        }

        private void addCardBtn_Click(object sender, RoutedEventArgs e)
        {
            //WindowTopUp topup = new WindowTopUp();
            //topup.ShowDialog();
            //if (topup.ShowDialog() == true)
            //{
            //    string str = topup.topupTextBox.Text;
            //    if (str.All(char.IsDigit))
            //    {
            //        RublesCard rublesCard = new RublesCard
            //        {   
            //            ClientId = 1,
            //            CardName = topup.nameTextBox.Text,
            //            CardBalance = Convert.ToDecimal(topup.topupTextBox.Text)
            //        };

            //        db.RublesCards.Add(rublesCard);
            //        db.SaveChanges();
            //        cardList.Items.Refresh();
            //    }
            //    else MessageBox.Show("Неверные данные");
            //}
        }

        private void topUpCardBtn_Click(object sender, RoutedEventArgs e)
        {
            RublesCard? rublesCard = cardList.SelectedItem as RublesCard;
            if (rublesCard is null) return;
            rublesCard.CardBalance = Convert.ToDecimal(value3.Text);

            rublesCard = db.RublesCards.Find(rublesCard.Id);
            db.SaveChanges();
            cardList.Items.Refresh();

            //WindowTopUp topup = new WindowTopUp();
            //topup.ShowDialog();
            //if (topup.ShowDialog() == true)
            //{
            //    RublesCard? rublesCard = cardList.SelectedItem as RublesCard;
            //    if (rublesCard is null) return;
            //    rublesCard.CardBalance += Convert.ToDecimal(topup.topupTextBox.Text);

            //    rublesCard = db.RublesCards.Find(rublesCard.Id);
            //    db.SaveChanges();
            //    cardList.Items.Refresh();
            //}
        }
        private void cardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RublesCard rublesCard = (RublesCard)sender;
            RublesCard? rublesCard = cardList.SelectedItem as RublesCard;
            if (rublesCard is null) return;
            value1.Text = rublesCard.ClientId.ToString();
            value2.Text = rublesCard.CardName;
            value3.Text = rublesCard.CardBalance.ToString();
        }
    }
}