using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace wallet221_wpf_proj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Создание контекста для БД
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
            historyList.DataContext = db.Histories.Local.ToObservableCollection().OrderByDescending(h => h.Id);
        }

        /// <summary>
        /// Метод для кнопки добавления новой карты и запись операции в таблицу История
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Метод для кнопки удаления карты и запись операции в таблицу История
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteCardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Метод для кнопки пополнения карты и запись операции в таблицу История
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topUpCardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Метод выбора карты из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Метод расчета прибыли по вкладу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void profitOfDepositBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RublesDeposit? rublesDeposit = depositList.SelectedItem as RublesDeposit;
                if (rublesDeposit is null) return;
                float profit;
                float balance = Convert.ToSingle(rublesDeposit.DepositBalance);
                float percent = Convert.ToSingle(rublesDeposit.DepositPercent);
                profit = balance / 100 * percent;
                MessageBoxResult result = MessageBox.Show($"Прибыль по вкладу за один год составит: {profit} рублей");
            }
            catch ( Exception ex ) 
            { 
                MessageBox.Show(ex.Message);
            }
           
        }

        /// <summary>
        /// Метод выбора депозита из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void depositList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Метод перевода денег с выбранной карты на депозит и запись операции в историю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void transferDepositBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowTransferMoney transferMoney = new WindowTransferMoney();
                transferMoney.ShowDialog();

                RublesCard? rublesCard = cardList.SelectedItem as RublesCard;
                if (rublesCard is null) return;
                rublesCard.CardBalance -= Convert.ToDecimal(transferMoney.transferTextBox.Text);

                rublesCard = db.RublesCards.Find(rublesCard.Id);
                db.SaveChanges();
                cardList.Items.Refresh();

                RublesDeposit? rublesDeposit = depositList.SelectedItem as RublesDeposit;
                if (rublesDeposit is null) return;
                rublesDeposit.DepositBalance += Convert.ToDecimal(transferMoney.transferTextBox.Text);

                rublesDeposit = db.RublesDeposits.Find(rublesDeposit.Id);
                db.SaveChanges();
                cardList.Items.Refresh();

                History newHistory = new History()
                {
                    ClientId = 1,
                    Operation = $"Перевод с карты: {rublesCard?.CardName} || Баланс: {rublesCard?.CardBalance}\nна вклад: {rublesDeposit?.DepositName} || Баланс: {rublesDeposit?.DepositBalance}",
                    CreateAt = DateTime.Now
                };

                db.Histories.Add(newHistory);
                db.SaveChanges();
                historyList.Items.Refresh();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }            
        }

        /// <summary>
        /// Метод снятия денег с депозита и запись операции в историю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void withdrawalDepositBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowWithdrawal withdrawal = new WindowWithdrawal();
                withdrawal.ShowDialog();

                RublesDeposit? rublesDeposit = depositList.SelectedItem as RublesDeposit;
                if (rublesDeposit is null) return;
                rublesDeposit.DepositBalance -= Convert.ToDecimal(withdrawal.withdrawalTextBox.Text);

                rublesDeposit = db.RublesDeposits.Find(rublesDeposit.Id);
                db.SaveChanges();
                cardList.Items.Refresh();

                History newHistory = new History()
                {
                    ClientId = 1,
                    Operation = $"Снятие денег с депозита || Баланс: {rublesDeposit?.DepositBalance}",
                    CreateAt = DateTime.Now
                };

                db.Histories.Add(newHistory);
                db.SaveChanges();
                historyList.Items.Refresh();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);  
            }    
        }
    }
}