/**
 * Простой пример работы с отключенным набором данных
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SimpleDatasetSql
{
   internal class Program
   {
      private const string ConnectionStringName = "Northwind";

      private static readonly string ConnectionString =
         ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;

      private static void Main()
      {
         const string selectSql = "SELECT ContactName, CompanyName FROM Customers";
         using (var connection = new SqlConnection(ConnectionString))
         {
            var dataAdapter = new SqlDataAdapter(selectSql, connection);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Customers");

            foreach (DataRow row in dataSet.Tables["Customers"].Rows)
            {
               Console.WriteLine("'{0}' from {1}", row[0], row[1]);
            }
         }
      }
   }
}