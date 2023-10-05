using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
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
        /// <summary>
        /// Создание нового контекста для БД
        /// </summary>
        WalletDbContext db = new WalletDbContext();
        /// <summary>
        /// Главное окно
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        /// <summary>
        /// Метод загрузки таблиц в Главное окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            db.Histories.Load();
            historyList.DataContext = db.Histories.Local.ToObservableCollection();
        }

        /// <summary>
        /// Метод добавления новой карты и запись операции в таблицу История
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCardBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowAddCard addcard = new WindowAddCard();
            addcard.ShowDialog();

            RublesCard rublesCard = new RublesCard()
            {
                ClientId = 1,
                CardName = addcard.addcardTextBox.Text,
                CardBalance = 0
            };

            db.RublesCards.Add(rublesCard);
            db.SaveChanges();
            cardList.Items.Refresh();

            History newHistory = new History()
            {
                ClientId = 1,
                Operation = $"Новая карта: {rublesCard?.CardName} || Баланс: {rublesCard?.CardBalance}",
                CreateAt = DateTime.Now
            };

            db.Histories.Add(newHistory);
            db.SaveChanges();
            historyList.Items.Refresh();
        }

        /// <summary>
        /// Метод удаления карты и запись операции в таблицу История
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteCardBtn_Click(object sender, RoutedEventArgs e)
        {
            RublesCard? rublesCard = cardList.SelectedItem as RublesCard;
            if (rublesCard is null) return;

            db.RublesCards.Remove(rublesCard);
            db.SaveChanges();
            cardList.Items.Refresh();

            History newHistory = new History()
            {
                ClientId = 1,
                Operation = $"Карта: {rublesCard.CardName} - удалена",
                CreateAt = DateTime.Now
            };

            db.Histories.Add(newHistory);
            db.SaveChanges();
            historyList.Items.Refresh();
        }

        /// <summary>
        /// Метод пополнения карты и запись операции в таблицу История
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topUpCardBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowTopUp topup = new WindowTopUp();
            topup.ShowDialog();

            RublesCard? rublesCard = cardList.SelectedItem as RublesCard;
            if (rublesCard is null) return;
            rublesCard.CardBalance += Convert.ToDecimal(topup.topupTextBox.Text);

            rublesCard = db.RublesCards.Find(rublesCard.Id);
            db.SaveChanges();
            cardList.Items.Refresh();

            History newHistory = new History()
            {
                ClientId = 1,
                Operation = $"Пополнение карты: {rublesCard?.CardName} || Баланс: {rublesCard?.CardBalance}",
                CreateAt = DateTime.Now
            };

            db.Histories.Add(newHistory);
            db.SaveChanges();
            historyList.Items.Refresh();
        }
        
        /// <summary>
        /// Метод выбора карты из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}