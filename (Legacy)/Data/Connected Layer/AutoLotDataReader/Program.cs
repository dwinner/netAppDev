using System;
using System.Data.SqlClient;

namespace AutoLotDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Data Readers *****\n");

            // Создать строку соединения через строитель
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
               {
                  InitialCatalog = "AutoLot",
                  DataSource = @"Hi-Tech-PC",
                  ConnectTimeout = 30,
                  IntegratedSecurity = true,
                  Pooling = false
               };                        

            // Создать и открыть подключение.
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
                sqlConnection.Open();
                ShowConnectionStatus(sqlConnection);

                // Создание команд SQL
                string strSQL = "Select * From Inventory;Select * from Customers";

                using (SqlCommand myCommand = new SqlCommand(strSQL, sqlConnection))
                {
                    #region iterate over the inventory & customers
                    // Obtain a data reader a la ExecuteReader().
                    using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                    {
                        do
                        {
                            while (myDataReader.Read())
                            {
                                Console.WriteLine("***** Record *****");
                                for (int i = 0; i < myDataReader.FieldCount; i++)
                                {
                                    Console.WriteLine("{0} = {1}",
                                         myDataReader.GetName(i),
                                         myDataReader.GetValue(i));
                                }
                                Console.WriteLine();
                            }
                        }
                        while (myDataReader.NextResult());
                    }
                    #endregion
                }
            }
            Console.ReadLine();
        }

        #region Helper function
        static void ShowConnectionStatus(SqlConnection cn)
        {
            // Show various stats about current connection object.
            Console.WriteLine("***** Info about your connection *****");
            Console.WriteLine("Database location: {0}", cn.DataSource);
            Console.WriteLine("Database name: {0}", cn.Database);
            Console.WriteLine("Timeout: {0}", cn.ConnectionTimeout);
            Console.WriteLine("Connection state: {0}\n", cn.State.ToString());
        }
        #endregion
    }
}
