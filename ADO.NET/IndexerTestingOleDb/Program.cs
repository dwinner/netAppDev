/**
 * Простой тест производительности операций на объектах чтения данных
 */

using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;

namespace IndexerTestingOleDb
{
   internal class Program
   {
      private static void Main()
      {
         string source = string.Format("Provider=SQLOLEDB;{0}", GetDatabaseConnection());
         const string @select = "SELECT CategoryID,CategoryName FROM	Categories";

         Console.WriteLine("\tOLE DB Readers");
         using (var conn = new OleDbConnection(source))
         {
            conn.Open();
            var cmd = new OleDbCommand(select, conn);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
               reader.Read();               
               const int maxIterations = 1000000;
               var categoryId = (int) reader[0];
               categoryId = (int) reader["CategoryId"];
               categoryId = reader.GetInt32(0);

               MethodTimer.TimeMethod(() => categoryId = (int) reader[0], maxIterations,
                  "{0} iterations	using numeric indexer :	{1}ms");
               MethodTimer.TimeMethod(() => categoryId = (int) reader["CategoryId"], maxIterations,
                  "{0} iterations	using string indexer :	{1}ms");
               MethodTimer.TimeMethod(() => categoryId = reader.GetInt32(0), maxIterations,
                  "{0} iterations	using GetInt32() :	{1}ms");
            }
         }

         Console.WriteLine("\tSQL Client Readers");
         using (var conn = new SqlConnection(GetDatabaseConnection()))
         {
            var cmd = new SqlCommand(select, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            const int maxIterations = 1000000;
            var categoryId = (int) reader[0];
            categoryId = (int) reader["CategoryId"];
            categoryId = reader.GetInt32(0);
            MethodTimer.TimeMethod(() => categoryId = (int) reader[0], maxIterations,
               "{0} iterations	using numeric indexer :	{1}ms");
            MethodTimer.TimeMethod(() => categoryId = (int) reader["CategoryId"], maxIterations,
               "{0} iterations	using string indexer :	{1}ms");
            MethodTimer.TimeMethod(() => categoryId = reader.GetInt32(0), maxIterations,
               "{0} iterations	using GetInt32() :	{1}ms");
         }
      }

      private static string GetDatabaseConnection()
      {
         return
            @"Data Source=DOTNET\DWINNER;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=bboytronik1985_DWINNER;Connect Timeout=30";
      }
   }

   public class MethodTimer
   {
      public static void TimeMethod(Action method, int iterations, string message)
      {
         Stopwatch sw = Stopwatch.StartNew();
         for (int i = 0; i < iterations; i++)
            method();
         sw.Stop();
         Console.WriteLine(message, iterations, sw.ElapsedMilliseconds);
      }
   }
}