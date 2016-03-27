using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace SqlSamplesUsingAdventureWorks
{
   public class StoredProcedures
   {
      /// <summary>
      /// CLR-хранимая процедура
      /// </summary>
      /// <param name="customerId">customerId</param>
      [SqlProcedure]
      public static void GetCustomerOrdersClr(int customerId)
      {
         using (var advConnection = new SqlConnection("Context Connection=true"))
            // Используем соединение, которое уже было открыто клиентом
         {
            advConnection.Open();

            var command = new SqlCommand
            {
               Connection = advConnection,
               CommandText =
                  "SELECT SalesOrderID, OrderDate, DueDate, ShipDate FROM Sales.SalesOrderHeader WHERE (CustomerID = @CustomerID) ORDER BY SalesOrderID"
            };
            command.Parameters.Add("@CustomerID", SqlDbType.Int);
            command.Parameters["@CustomerID"].Value = customerId;

            SqlDataReader reader = command.ExecuteReader();
            SqlPipe pipe = SqlContext.Pipe;
            if (pipe != null)
               pipe.Send(reader);
         }
      }
   }
}