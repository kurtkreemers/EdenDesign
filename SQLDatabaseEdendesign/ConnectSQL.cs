using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace SQLDatabaseEdendesign
{
    public class ConnectSQL
    { 
       
        static SqlDataAdapter sda;
        static SqlCommand cmd;
        static SqlConnection sqlConn =  new SqlConnection (SQLDatabaseEdendesign.Properties.Settings.Default.ConnectionString);

        
        public static DataTable GetData(string selectedTable, string searchInstruction)
        {
            string query = "SELECT * FROM " + selectedTable + searchInstruction;
            sqlConn.Open();
            cmd = new SqlCommand(query, sqlConn);
            sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sqlConn.Close();
            return dt;
        }
        public static DataSet BindGrid(string selectedTable)
        {
            sqlConn.Open();
            SqlDataAdapter _Adapter = new SqlDataAdapter("Select * from " + selectedTable, sqlConn);
            DataSet _Bind = new DataSet();
            _Adapter.Fill(_Bind, "MyDataBinding");
            sqlConn.Close();
            return _Bind;
        }
        public static void InsertRow(string command)
        {
            
            cmd.CommandText = command;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConn;  
            sqlConn.Open();
            cmd.ExecuteNonQuery();

        }
                
    }

}
