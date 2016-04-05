/**
 * Заполнение набора данных
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAdapterDemo
{
   internal class Program
   {
      private const string ConnectionStringName = "Northwind";

      private static readonly string ConnectionString =
         ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;

      private static void Main()
      {
         // Создаем и открываем соединение с базой данных
         using (var sqlConnection = new SqlConnection(ConnectionString))
         {
            sqlConnection.Open();
            var dataSet = new DataSet(); // Создаем набор данных            
            var sqlDataAdapter = new SqlDataAdapter { SelectCommand = GenerateSelectCommand(sqlConnection) };   // Создаем адаптер данных для заполнения набора
            sqlDataAdapter.Fill(dataSet, "Region");   // Заполнение набор данных через адаптер
            
            Console.WriteLine("Data selected via a stored procedure");
            foreach (DataRow dataRow in dataSet.Tables["Region"].Rows)
            {
               Console.WriteLine("  {0,-3} {1}", dataRow[0], dataRow[1]);
            }

            sqlConnection.Close();
         }
      }

      private static SqlCommand GenerateSelectCommand(SqlConnection aSqlConnection)
      {
         return new SqlCommand("RegionSelect", aSqlConnection)
         {
            CommandType = CommandType.StoredProcedure,
            UpdatedRowSource = UpdateRowSource.None
         };
      }
   }
}