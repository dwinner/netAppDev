/**
 * Выполнение хранимой процедуры
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _06_StoredProc
{
   class Program
   {
      private static readonly string BooksConnStr = ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;

      static void Main()
      {
         try
         {
            using (var conn = new SqlConnection(BooksConnStr))
            {
               conn.Open();
               using (var sqlCmd = new SqlCommand("GetSortedBooks", conn))
               {
                  // Мы указываем тип команды, чтобы объект-команда интерпретировал
                  // текст запроса как хранимую процедуру
                  sqlCmd.CommandType = CommandType.StoredProcedure;
                  using (SqlDataReader reader = sqlCmd.ExecuteReader())
                  {
                     // SqlDataReader считывает по одной строке из базы данных
                     // по мере того, как вы их запрашиваете
                     while (reader.Read())
                     {
                        Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                     }
                  }
               }            
            }
         }
         catch (SqlException sqlException)
         {
            Console.WriteLine(sqlException);
         }

         Console.ReadLine();
      }
   }
}
