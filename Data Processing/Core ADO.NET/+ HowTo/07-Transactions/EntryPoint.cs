/**
 * Транзакции
 */

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace _07_Transactions
{
   class EntryPoint
   {
      private const string TRN_RIGHT_INSERT = "INSERT INTO Books (Title, PublishYear) VALUES ('Test', 2010)";
      private const string TRN_WRONG_INSERT = "INSERT INTO Books (Title, PublishYear) VALUES ('Test', 'Oops')";
      private static readonly string BooksConStr = ConfigurationManager.ConnectionStrings["BooksDb"].ConnectionString;

      static void Main()
      {
         using (var sqlConn = new SqlConnection(BooksConStr))
         {
            sqlConn.Open();
            Console.WriteLine("Before attempted inserts: ");
            using (var sqlCmd = new SqlCommand("SELECT * FROM Books", sqlConn))
            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
               while (reader.Read())
               {
                  Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
               }
            }
            using (SqlTransaction transaction = sqlConn.BeginTransaction())
            {
               try
               {
                  using (var cmd = new SqlCommand(TRN_RIGHT_INSERT, sqlConn, transaction))
                  {
                     cmd.ExecuteNonQuery();  // Это должно работать
                  }
                  using (var cmd = new SqlCommand(TRN_WRONG_INSERT, sqlConn, transaction))
                  {
                     cmd.ExecuteNonQuery();  // Это НЕ должно работать
                  }
                  // Если программа дошла до этого места, значит,
                  // все в порядке, можно продолжить и выполнить транзакцию
                  transaction.Commit();
               }
               catch (SqlException)
               {
                  Console.WriteLine("Exception occured, rolling back");
                  transaction.Rollback();
               }
            }
         }

         Console.ReadKey();
      }
   }
}
