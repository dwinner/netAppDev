using System.Data;
using System.Data.SqlClient;

namespace EF;

public abstract class IntegrationTests
{
   protected const string ConnectionString = @"Server=.\Sql;Database=IntegrationTests;Trusted_Connection=true;";

   protected IntegrationTests()
   {
      ClearDatabase();
   }

   private void ClearDatabase()
   {
      var query =
         "DELETE FROM dbo.[User];" +
         "DELETE FROM dbo.Company;";

      using (var connection = new SqlConnection(ConnectionString))
      {
         var command = new SqlCommand(query, connection)
         {
            CommandType = CommandType.Text
         };

         connection.Open();
         command.ExecuteNonQuery();
      }
   }
}