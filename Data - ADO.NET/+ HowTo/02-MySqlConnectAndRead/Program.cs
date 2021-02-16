/**
 * Подключение к MySQL
 */

using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace _02_MySqlConnectAndRead
{
   class Program
   {
      private const string SELECT_FROM_BOOKS = "SELECT * FROM books";

      private static readonly string BooksConnStr =
         ConfigurationManager.ConnectionStrings["MysqlBooks"].ConnectionString;

      static void Main()
      {
         try
         {
            using (var conn = new MySqlConnection(BooksConnStr))
            {
               conn.Open();
               using (var cmd = new MySqlCommand(SELECT_FROM_BOOKS, conn))
               using (MySqlDataReader reader = cmd.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                  }
               }
            }
         }
         catch (MySqlException mysqlEx)
         {
            Console.WriteLine(mysqlEx);
         }

         Console.ReadKey();
      }
   }
}
