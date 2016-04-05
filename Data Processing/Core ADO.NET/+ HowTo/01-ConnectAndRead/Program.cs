/**
 * Соединение с базой и чтение данных
 */

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace _01_ConnectAndRead
{
   class Program
   {
      private const string SELECT_ALL_BOOKS_CMD = "SELECT * FROM Books";

      private static readonly string TestDbConnectionString =
         ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;

      static void Main()
      {
         try
         {
            using (var sqlConnection = new SqlConnection(TestDbConnectionString))
            {
               sqlConnection.Open();

               // Не забудьте передать команде объект-соединение
               using (var cmd = new SqlCommand(SELECT_ALL_BOOKS_CMD, sqlConnection))
               using (SqlDataReader reader = cmd.ExecuteReader())
               {
                  // Класс SqlDataReader читает ряды и базы данных
                  // по одному, по мере того, как вы их запрашиваете
                  while (reader.Read())
                  {
                     Console.WriteLine("{0}\t{1}\t{2}",
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2)
                     );
                  }
               }
            }
         }
         catch (SqlException sqlEx)
         {
            Console.WriteLine(sqlEx);
         }

         Console.ReadKey();
      }
   }
}
