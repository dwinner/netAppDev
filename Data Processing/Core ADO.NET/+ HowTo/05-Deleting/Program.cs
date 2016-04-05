/**
 * Удаление
 */

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace _05_Deleting
{
   class Program
   {
      private const string DELETE_CMD = "DELETE FROM Books WHERE Title LIKE '%Test'";
      private static readonly string BooksConnStr = ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;

      static void Main()
      {
         try
         {
            using (var sqlConn = new SqlConnection(BooksConnStr))
            {
               sqlConn.Open();
               // Удалить строки, вставленные ранее
               using (var sqlCmd = new SqlCommand(DELETE_CMD, sqlConn))
               {
                  int rowsAffected = sqlCmd.ExecuteNonQuery();
                  Console.WriteLine("{0} rows affect by delete", rowsAffected);
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
