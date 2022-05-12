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
using assignment4.Models;
using assignment4.Utilities;


namespace assignment4
{
    /// <summary>
    /// Interaction logic for transactionWindow.xaml
    /// </summary>
    public partial class transactionWindow : Window
    {
        private Account selectedAccount;
        private Records selectedRecord;
        public transactionWindow(Account selectedAccount)
        {
            InitializeComponent();
            this.selectedAccount = selectedAccount;
            InitializeWindow();
        }
        private void InitializeWindow()
        {

            userTitle.Content = $"Hello, {selectedAccount.userName}";
            balance.Text = Convert.ToString(selectedAccount.Balance);
            recordsBox.ItemsSource = selectedAccount.Records;
            txtRecordId.Text = this.GenerateRecordId();
            recordsBox.SelectionChanged += this.OnSelectRecord;
        }

        private void GenerateRecords(object sender, EventArgs e)
        {
            foreach (var column in ((DataGrid)sender).Columns)
            {
                column.MinWidth = 130;
                if (column.Header.ToString() == "RecordsId")
                {
                    column.Header = "Records ID";
                }
                else if (column.Header.ToString() == "ProcessedDate")
                {
                    column.Header = "Date";
                }
            }

        }
        private string GenerateRecordId()
        {
            return new Random().Next(10000, 99999).ToString();
        }

        private void ResetTable(object sender, RoutedEventArgs e)
        {
            txtRecordId.Text = this.GenerateRecordId();
            Rtype.SelectedItem = null;
            txtValue.Text = null;
            txtDescription.Text = null;
        }

        private void AddRecords(object sender, RoutedEventArgs e)
        {
            if (Rtype.SelectedItem == null || string.IsNullOrWhiteSpace(txtValue.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please input all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                double value;
                try
                {
                    value = double.Parse(txtValue.Text);
                }
                catch
                {

                    MessageBox.Show("Value must be a positive numeric value.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (value < 0)
                {
                    MessageBox.Show("Value cannot be negative.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
                Records record = new Records();
                record.RecordsId = txtRecordId.Text;
                if (((ComboBoxItem)Rtype.SelectedItem).Content.ToString() == "Expense")
                {
                    record.Type = Records.RecordsType.Expense;
                }
                else
                {
                    record.Type = Records.RecordsType.Income;
                }
                record.Value = value;
                record.Description = txtDescription.Text;
                selectedAccount.Records.Add(record);
                try
                {
                    AccountManager.Update(selectedAccount);
                    this.ResetTable(sender, e);
                    recordsBox.ItemsSource = null;
                    recordsBox.ItemsSource = selectedAccount.Records;
                    balance.Text = null;
                    balance.Text = Convert.ToString(selectedAccount.Balance);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageBox.Show("Record added sucessfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OnSelectRecord(object sender, SelectionChangedEventArgs e)
        {
            this.selectedRecord = ((DataGrid)sender).SelectedItem as Records;

            if (this.selectedRecord == null)
            {
                return;
            }
            else
            {
                if (MessageBox.Show("Do you want to delete this record?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    for (int i = 0; i < this.selectedAccount.Records.Count; i++)
                    {
                        if (this.selectedAccount.Records[i] == this.selectedRecord)
                        {
                            this.selectedAccount.Records.RemoveAt(i);
                        }
                    }
                }
                this.ResetTable(sender, e);
                recordsBox.ItemsSource = null;
                recordsBox.ItemsSource = selectedAccount.Records;
                balance.Text = null;
                balance.Text = Convert.ToString(selectedAccount.Balance);

            }
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            if (incomeCheck.IsChecked == true && expenseCheck.IsChecked == false)
            {
                IEnumerable<Records> query = selectedAccount.Records.Where(record => Convert.ToString(record.Type) == "Income");
                IEnumerable<Records> query1 = KeyWordSearch(ref query);
                recordsBox.ItemsSource = null;
               // recordsBox.ItemsSource = query1;
                recordsBox.ItemsSource = ValueSearch(ref query1);


            }
            else if (expenseCheck.IsChecked == true && incomeCheck.IsChecked == false)
            {
                IEnumerable<Records> query = selectedAccount.Records.Where(record => Convert.ToString(record.Type) == "Expense");
                IEnumerable<Records> query1 = KeyWordSearch(ref query);
                recordsBox.ItemsSource = null;
                recordsBox.ItemsSource = ValueSearch(ref query1);
               // incomeCheck.IsChecked = false;
              
            }
            else if (incomeCheck.IsChecked == true && incomeCheck.IsChecked == true)
            {
                IEnumerable<Records> query = selectedAccount.Records.Where(record => Convert.ToString(record.Type) != null);
                IEnumerable<Records> query1 = KeyWordSearch(ref query);
                recordsBox.ItemsSource = null;
                recordsBox.ItemsSource = ValueSearch(ref query1);
            }
        }

        private void Reset_U(object sender, RoutedEventArgs e)
        {
            txtRecordId2.Text = null;
            tValue.Text = null;
            td.Text = null;
        }

        private void UpdateRecords(object sender, RoutedEventArgs e)
        {
            bool x = false;
            if (radioIncome.IsChecked == false && radioExpense.IsChecked == false || string.IsNullOrWhiteSpace(tValue.Text) || string.IsNullOrWhiteSpace(td.Text))
            {
                MessageBox.Show("Please input all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                double value;
                try
                {
                    value = double.Parse(tValue.Text);
                }
                catch
                {

                    MessageBox.Show("Value must be a positive numeric value.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (value < 0)
                {
                    MessageBox.Show("Value cannot be negative.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }


                for (int i = 0; i < this.selectedAccount.Records.Count; i++)
                {
                    if (this.selectedAccount.Records[i].RecordsId == txtRecordId2.Text)
                    {
                        x = true;
                        this.selectedAccount.Records[i].Value = double.Parse(tValue.Text);
                        this.selectedAccount.Records[i].Description = td.Text;
                        if (radioIncome.IsChecked == true)
                        {
                            this.selectedAccount.Records[i].Type = Records.RecordsType.Income;
                        }
                        else if (radioExpense.IsChecked == true)
                        {
                            this.selectedAccount.Records[i].Type = Records.RecordsType.Expense;
                        }
                        AccountManager.Update(selectedAccount);
                        //this.ResetTable(sender, e);
                        recordsBox.ItemsSource = null;
                        recordsBox.ItemsSource = selectedAccount.Records;
                        balance.Text = null;
                        balance.Text = Convert.ToString(selectedAccount.Balance);
                        Reset_U( sender, e);
                        MessageBox.Show("Record updatded sucessfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                if (x == false)
                {
                    MessageBox.Show("Record Id does not exist!");
                }
            }
        }

        private IEnumerable<Records> KeyWordSearch(ref IEnumerable<Records> query)
        {
           IEnumerable<Records> queryN;
           string keyword = keyWords.Text.ToLower();
           queryN = query.Where(record => record.Description.Contains(keyword))
                                                                .OrderBy(record => record.ProcessedDate);
            return queryN;
        }

        private IEnumerable<Records> ValueSearch(ref IEnumerable<Records> query) {
            IEnumerable<Records> queryV;
            double valueLeft;
            double valueRight;
            if (string.IsNullOrWhiteSpace(valueL.Text))
            {
                valueLeft = 0;

            }
            else {
               valueLeft = double.Parse(valueL.Text);
            }

            if (string.IsNullOrWhiteSpace(valueR.Text))
            {
                valueRight = double.MaxValue;
            }
            else { 
                valueRight = double.Parse(valueR.Text);
            }
             
                queryV = query.Where(record => record.Value >= valueLeft && record.Value <= valueRight)
                              .OrderBy(record => record.Value);
                return queryV;
            }

        private void ShowAll(object sender, RoutedEventArgs e)
        {
            recordsBox.ItemsSource = null;
            recordsBox.ItemsSource = selectedAccount.Records;
            expenseCheck.IsChecked = false;
            incomeCheck.IsChecked = false;
            keyWords.Text = null;
            valueL.Text = null;
            valueR.Text = null;
        }
    }
}



    
