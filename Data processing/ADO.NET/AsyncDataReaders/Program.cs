/**
 * Асинхронные операции доступа к данным
 */

using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AsyncDataReaders
{
   internal class Program
   {
      private static void Main()
      {
         Console.WriteLine("Running tasks...");

         MethodTimer.TimeMethod(() =>
         {
            Task<int> employeeCountTask = GetEmployeeCount();
            Task<int> ordersCountTask = GetOrdersCount();
            Task.WaitAll(employeeCountTask, ordersCountTask);
            Console.WriteLine("Number of employes: {0}, Number of orders: {1}", employeeCountTask.Result,
               ordersCountTask.Result);
         }, 1, "Getting data took {1}ms");
      }

      public static async Task<int> GetOrdersCount()
      {
         using (var connection = new SqlConnection(GetDatabaseConnection()))
         {
            var command = new SqlCommand("WAITFOR DELAY '0:0:02';SELECT COUNT(*) FROM ORDERS", connection);
            connection.Open();

            return await command.ExecuteScalarAsync().ContinueWith(task => Convert.ToInt32(task.Result));
         }
      }

      public static async Task<int> GetEmployeeCount()
      {
         using (var connection = new SqlConnection(GetDatabaseConnection()))
         {
            var command = new SqlCommand("WAITFOR DELAY '0:0:02';SELECT COUNT(*) FROM employees", connection);
            connection.Open();

            return await command.ExecuteScalarAsync().ContinueWith(task => Convert.ToInt32(task.Result));
         }
      }

      public static async Task GetEmployeesAndOrders()
      {
         int employees = await GetEmployeeCount();
         int orders = await GetOrdersCount();

         Console.WriteLine("Number of employes: {0}, Number of orders: {1}", employees, orders);
      }

      private static string GetDatabaseConnection()
      {
         return
            @"Data Source=DOTNET\DWINNER;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=bboytronik1985_DWINNER;Connect Timeout=30";
      }
   }
}