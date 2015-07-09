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

        public static DataTable GetAllColumns(string selectedDatabase)
       {      
            string query = "SELECT * FROM " + selectedDatabase;           
            sqlConn.Open();
            cmd = new SqlCommand(query, sqlConn);
            SqlDataReader queryCommandReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(queryCommandReader);
            sqlConn.Close();
            return dataTable;
        }
        
        public static DataTable GetData(string selectedDatabase, string searchInstruction)
        {
            string query = "SELECT * FROM " + selectedDatabase + searchInstruction;
            sqlConn.Open();
            cmd = new SqlCommand(query, sqlConn);
            sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sqlConn.Close();
            return dt;
        }
        public static DataSet BindGrid(string selectedDatabase)
        {
            sqlConn.Open();
            SqlDataAdapter _Adapter = new SqlDataAdapter("Select * from " + selectedDatabase, sqlConn);
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
