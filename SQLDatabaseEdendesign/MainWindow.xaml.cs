using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLDatabaseEdendesign.Enums;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Threading;


namespace SQLDatabaseEdendesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (DatabaseName name in Enum.GetValues(typeof(DatabaseName)))
            {
                DatabaseSelect.Items.Add(name);
            }
        }
        DataTable dt;
        List<string> columnNames = new List<string>();
        private void DatabaseSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColumnsComboBox.Items.Clear();
            SQLDataGrid.Columns.Clear();
            SearchBox.Text = "";
            SQLDataGrid.ItemsSource = null;
            SqlRecordsCount.Content = null;
            string selectedDataBase = DatabaseSelect.SelectedItem.ToString();
            dt = ConnectSQL.GetData(selectedDataBase,"");

            foreach (DataColumn columnSQL in dt.Columns)
            {
                string columnName = columnSQL.ToString();
                if (!columnSQL.ToString().ToUpper().Contains("USER") && !columnSQL.ToString().ToUpper().Contains("VOL"))
                {
                    ColumnsComboBox.Items.Add(columnName);
                    columnNames.Add(columnName);
                }
            }
            SQLDataGrid.ItemsSource = dt.DefaultView;
        }
        
        private void Search_Click(object sender, RoutedEventArgs e)
        {

                if (ColumnsComboBox.SelectedItem != null && SearchBox.Text != "")
                {
                    string selectedDataBase = DatabaseSelect.SelectedItem.ToString();
                    string selectedColumn = ColumnsComboBox.SelectedItem.ToString();                    
                    string asterix = "";                
                    string searchText;
                    int length = SearchBox.Text.Length - 1;
                    if (SearchBox.Text[0] == '*')
                    {
                        asterix = "%";
                        searchText = SearchBox.Text.Substring(1, length);                        
                    }                 
                    else
                    {
                        asterix = "";      
                        searchText = SearchBox.Text;
                    }
                    string searchInstruction = " WHERE " + selectedColumn + " LIKE '" + asterix + searchText + "%'";
                    dt = ConnectSQL.GetData(selectedDataBase, searchInstruction);
                    SQLDataGrid.ItemsSource = dt.DefaultView;
                    foreach (var column in SQLDataGrid.Columns)
                    {
                        column.IsReadOnly = true;
                    }
                    SqlRecordsCount.Content = "Records : " + dt.Rows.Count.ToString();
                }

        }

        private void SQLDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString().ToUpper().Contains("USER") || e.Column.Header.ToString().ToUpper().Contains("VOL"))
            {
                e.Column.Visibility = Visibility.Hidden;
            }
        }

        //private void Add_Record(object sender, RoutedEventArgs e)
        //{


        //    //string columnString = "";
        //    //foreach (var column in SQLDataGrid.Columns)
        //    //{
        //    //    columnString += column.Header.ToString() + ",";
        //    //}
        //    //columnString = columnString.Substring(0, columnString.Length - 1);


        //    

        //}

        private void Add_Record_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseSelect.SelectedItem != null)
            {
                Add_Record win = new Add_Record(DatabaseSelect.SelectedItem.ToString());
                win.Show();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            DataSet dataset = ConnectSQL.BindGrid(DatabaseSelect.SelectedItem.ToString());
            SQLDataGrid.DataContext = dataset;
        }

        private void SQLDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
  
            var row = (DataRowView)SQLDataGrid.SelectedValue;
            int id = (int)row.Row.ItemArray[0];

        }       

    }
}