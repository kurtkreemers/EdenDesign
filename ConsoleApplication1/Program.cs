using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = null;
    SqlConnection cnn ;
    connectionString = @"Data Source=.\SQLExpress;AttachDbFilename=D:\SQL Database test\EdenDesign.mdf;Database=EdenDesign;Trusted_Connection=Yes";
    cnn = new SqlConnection(connectionString);
    try
    {
        cnn.Open();

         using (SqlCommand cmd = new SqlCommand("SELECT * from [EdenDesign].[dbo].[JobNummers] where EindKlant like '%k%'", cnn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["JobNr"].ToString() + " " + reader["Klant"].ToString() + " " + reader["Jaar"].ToString() + " " + reader["EindKlant"].ToString());

                            }
                        }
                    } // reader closed and disposed up here

                } // command disposed here
       
        cnn.Close();
        Console.ReadLine();
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
           
        
    }
    }
}
