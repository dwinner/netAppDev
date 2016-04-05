/**
 * Использование хранимых процедур CLR
 */

using System;
using System.Data;
using System.Data.SqlClient;

namespace UsingClrStoredProc
{
   internal static class Program
   {
      private const string AdvConnectionString =
         @"Data Source=VINEVCEV-PC\BTRONIK;Initial Catalog=AdventureWorks;Integrated Security=True";

      private static void Main()
      {
         using (var connection = new SqlConnection(AdvConnectionString))
         {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "GetCustomerOrdersClr";
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@customerId", 11000);
            command.Parameters.Add(parameter);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
               Console.WriteLine("{0} {1:D}", reader["SalesOrderID"], reader["OrderDate"]);
            }
         }
      }
   }
}