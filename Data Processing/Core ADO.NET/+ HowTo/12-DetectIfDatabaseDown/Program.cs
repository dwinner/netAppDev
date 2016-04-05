/**
 * Выяснение доступности соединения с базой данных
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _12_DetectIfDatabaseDown
{
   class Program
   {
      private static readonly string BooksConnStr = ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;

      static void Main()
      {
         Console.WriteLine("DB is {0}", TestConnection() ? "OK" : "Down");
         Console.WriteLine("Try shutting down the database server and running this test again");

         Console.ReadKey();
      }

      private static bool TestConnection()
      {
         try
         {
            using (var conn = new SqlConnection(BooksConnStr))
            {
               conn.Open();
               return conn.State == ConnectionState.Open;
            }
         }
         catch (SqlException)
         {
            return false;
         }
         catch (InvalidOperationException)
         {
            return false;
         }
      }
   }
}
