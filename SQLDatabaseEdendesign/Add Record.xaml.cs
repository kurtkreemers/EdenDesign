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
using System.Windows.Shapes;
using System.Data;

namespace SQLDatabaseEdendesign
{
    /// <summary>
    /// Interaction logic for Add_Record.xaml
    /// </summary>
    public partial class Add_Record : Window
    {
        DataTable sqlTable;      
        public string SelectedDatabase { get; set; }
        List<string> columnsList = new List<string>();
        public Add_Record(string selectedDatabase)
        {
            InitializeComponent();
            this.SelectedDatabase = selectedDatabase;
            sqlTable = ConnectSQL.GetAllColumns(selectedDatabase);
            int i = 0;
            TextBox tb;
           
            foreach (DataColumn columnSQL in sqlTable.Columns)
            {
                columnsList.Add(columnSQL.ToString()); 
                if (!columnSQL.ToString().ToUpper().Contains("USER") && !columnSQL.ToString().ToUpper().Contains("VOL"))
                {
                    Label lb = new Label();
                    lb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    lb.Content = columnSQL.ToString() + " :";
                    RecordStack.Children.Add(lb);
                    if (columnSQL.ToString().ToUpper().Contains("ACTIEF"))
                    {
                        CheckBox cb = new CheckBox();
                        cb.Name = "ActiefCheck";
                        cb.Margin = new Thickness(10, 0, 0, 0);
                        RecordStack.Children.Add(cb);
                    }
                    else
                    {
                        tb = new TextBox();
                        tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        tb.Name = "TextBox_" + i;
                        tb.Width = 250;
                        RecordStack.Children.Add(tb);
                    }
                    i += 1;
                }

            }
            for (int j = 0; j < VisualTreeHelper.GetChildrenCount(RecordStack); j++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(RecordStack, j);

                if (child is TextBox)
                {
                    TextBox castedChild = (TextBox)child;
                    if (castedChild.Name == "TextBox_0")
                        castedChild.Text = (sqlTable.Rows.Count + 1).ToString();
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string allTextBox = "";
            //for (int j = 0; j < VisualTreeHelper.GetChildrenCount(RecordStack); j++)
            //{
            //    DependencyObject child = VisualTreeHelper.GetChild(RecordStack, j);

            //    if (child is TextBox)
            //    {
            //        TextBox castedChild = (TextBox)child;
            //        allTextBox += castedChild.Text + ",";
            //    }
            //}

            string sqlCommand = "INSERT INTO " + SelectedDatabase + " (";
            foreach (string columnName in columnsList)
            {
                sqlCommand = sqlCommand + columnName + ",";
            }
            sqlCommand = sqlCommand.Substring(0, sqlCommand.Length - 1) + " VALUES (";

        }
    }
    }

