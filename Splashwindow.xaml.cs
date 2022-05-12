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
using assignment4.Utilities;
using assignment4.Models;


namespace assignment4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Account selectedAccount;
        
        
        public MainWindow()
        {
            InitializeComponent();
            accountBox.ItemsSource = AccountManager.ShowAll();
            accountBox.SelectionChanged +=OnSelectAccount;
        }

        public Account getSelectedAccount()
        {
            return this.selectedAccount;
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
            {
                MessageBox.Show("Please input your user name.");
                
            }
            else
            {
                Account account = new Account();
                account.userName = username.Text;

                try
                {
                    AccountManager.Create(account);
                    accountBox.ItemsSource = AccountManager.ShowAll();
                    MessageBox.Show("Account successfully created!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    username.Text = null;
                }
                catch (Exception error1)
                {
                    MessageBox.Show(error1.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GenerateAccountColumns(object sender, EventArgs e)
        {
            foreach (var column in ((DataGrid)sender).Columns)
            {
                column.MinWidth = 138;
                if (column.Header.ToString() == "AccountId")
                {
                    column.Header = "Account ID";
                }
                else if (column.Header.ToString() == "userName")
                {
                    column.Header = "User Name";
                }
                else if (column.Header.ToString() == "Records")
                {
                    column.Visibility = Visibility.Hidden;
                }
            }
        }

        private void OnSelectAccount(object sender, SelectionChangedEventArgs e)
        {
            this.selectedAccount = ((DataGrid)sender).SelectedItem as Account;

            if (this.selectedAccount == null)
            {
                return;
            }
            else {
                Window transactionWindow = new transactionWindow(selectedAccount);
                this.Close();
               transactionWindow.ShowDialog();
               
            }
        }
       
    }
}
