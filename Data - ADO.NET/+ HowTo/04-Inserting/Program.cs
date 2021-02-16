/**
 * Добаление данных в таблицу
 */

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace _04_Inserting
{
   class Program
   {
      private const string INSERT_CMD = "INSERT INTO Books (Title, PublishYear) VALUES (@Title, @PublishYear)";
      private static readonly string BooksConnStr = ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;

      static void Main()
      {
         try
         {
            using (var conn = new SqlConnection(BooksConnStr))
            {
               conn.Open();
               using (var insertCommand = new SqlCommand(INSERT_CMD, conn))
               {
                  insertCommand.Parameters.AddRange(new[]
                  {
                     new SqlParameter("@Title", "ASP.NET MVC 4"),
                     new SqlParameter("@PublishYear", "2013")
                  });
                  // Здесь используется метод ExecuteNonQuery, потому что
                  // мы не выдаем запрос на чтение строк во время вставки
                  int rowsAffected = insertCommand.ExecuteNonQuery();
                  Console.WriteLine("{0} rows affected by insert", rowsAffected);
               }
            }
         }
         catch (SqlException sqlException)
         {
            Console.WriteLine(sqlException);
         }

         Console.ReadKey();         
      }
   }
}
