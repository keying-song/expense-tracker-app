keying Song 301166693
expense tracker- WPF application, c#. This application help users to add, delete, update, and search for their income and expense records and also calculate the total balance.

1. this program contains Buttons, Labels, Testboxes,  Radio Buttons, Checkboxes, TabControls
2.this program contains Combobox
3.this program uses Queue, List
4. the data is read from xml
5.selectedAccount is been sent to Main Window.
6. technical difficulty:  users are able to create account and enter their account, create, update and delete expense/income records. This program will calculate the current balance. 
                          users are able to search keywords to find the records, or filter the records through type or value. 
7. linq queries:   
              
               IEnumerable<Records> queryV = query.Where(record => record.Value >= valueLeft && record.Value <= valueRight)
                              .OrderBy(record => record.Value);

                IEnumerable<Records> queryN = query.Where(record => record.Description.Contains(keyword))
                                                                .OrderBy(record => record.ProcessedDate);

                IEnumerable<Records> query = selectedAccount.Records.Where(record => Convert.ToString(record.Type) != null);
                IEnumerable<Records> query = selectedAccount.Records.Where(record => Convert.ToString(record.Type) == "Expense");
8. this program only contains .net generic collection  IEnumerable<Records> query.
  
  
![project1](https://user-images.githubusercontent.com/102123559/171959637-e565f811-4a84-4923-883e-c78869b9378d.gif)
