/**
 * Клиент для UDT SqlCoordinate
 */

using System;
using System.Data;
using System.Data.SqlClient;

namespace UdtClient
{
   internal static class Program
   {
      private static void Main()
      {
         const string connectionString = @"Data Source=VINEVCEV-PC\BTRONIK;Initial Catalog=SqlServerSampleDB;Integrated Security=True";
         using (var connection = new SqlConnection(connectionString))
         {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name, Location FROM Cities";
            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
               while (reader.Read())
               {
                  Console.WriteLine("{0,-10} {1}", reader[1], reader[2]);
               }
            }
         }
      }
   }
}