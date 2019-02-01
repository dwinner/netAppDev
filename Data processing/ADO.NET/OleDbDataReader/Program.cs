/**
 * Использование поставщика OLE
 */

using System;
using System.Data.OleDb;

namespace OleDbDataReader
{
   internal class Program
   {
      private static void Main()
      {         
         string source = string.Format("Provider=SQLOLEDB;{0}", GetDatabaseConnection());
         const string selectOperator = "SELECT ContactName, CompanyName FROM Customers";
         using (var oleDbConnection = new OleDbConnection(source))
         {
            oleDbConnection.Open();
            var oleDbCommand = new OleDbCommand(selectOperator, oleDbConnection);
            using (var oleDbDataReader = oleDbCommand.ExecuteReader())
            {
               while (oleDbDataReader != null && oleDbDataReader.Read())
               {
                  Console.WriteLine("'{0}' from {1}", oleDbDataReader.GetString(0), oleDbDataReader.GetString(1));
               }
               if (oleDbDataReader != null) oleDbDataReader.Close();
            }
            oleDbConnection.Close();
         }
      }

      private static string GetDatabaseConnection()
      {
         return
            @"Data Source=DOTNET\DWINNER;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=bboytronik1985_DWINNER;Connect Timeout=30";
      }
   }
}