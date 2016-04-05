/**
 * Генерация кода с помощью XSD
 */

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace XsdDataset
{
   internal class Program
   {
      private const string ConnectionStringName = "Northwind";

      private static readonly string ConnectionString =
         ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;

      private static void Main()
      {
         const string selectSql = "SELECT * FROM Products";
         using (var connection = new SqlConnection(ConnectionString))
         {
            var dataAdapter = new SqlDataAdapter(selectSql, connection);
            var products = new Products();
            dataAdapter.Fill(products, "Product");

            foreach (Products.ProductRow row in products.Product)
            {
               Console.WriteLine("'{0}' from {1}", row.ProductID, row.ProductName);
            }
         }
      }
   }
}