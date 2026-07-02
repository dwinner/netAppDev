using System.Data;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Data.SqlClient;
// To use ServerCallContext.
// To use SqlConnection and so on.
// To use CommandType.
// To use ByteString.

// To use ToTimestamp method.

namespace Northwind.Grpc.Service.Services;

public class EmployeeService(ILogger<EmployeeService> logger) : Employee.EmployeeBase
{
   private readonly ILogger<EmployeeService> _logger = logger;

   private async Task<SqlCommand> GetCommand()
   {
      SqlConnectionStringBuilder builder = new();

      builder.InitialCatalog = "Northwind";
      builder.MultipleActiveResultSets = true;
      builder.Encrypt = true;
      builder.TrustServerCertificate = true;
      builder.ConnectTimeout = 10; // Default is 30 seconds.
      builder.DataSource = "."; // To use local SQL Server.
      builder.IntegratedSecurity = true;

      /*
      // To use SQL Server Authentication:
      builder.UserID = userId;
      builder.Password = password;
      builder.PersistSecurityInfo = false;
      */

      SqlConnection connection = new(builder.ConnectionString);

      await connection.OpenAsync();

      var cmd = connection.CreateCommand();
      cmd.CommandType = CommandType.Text;
      return cmd;
   }

   private EmployeeReply ReaderToEmployee(SqlDataReader sqlDataReader)
   {
      EmployeeReply employeeReply = new()
      {
         EmployeeId = sqlDataReader.GetInt32("EmployeeId"),
         LastName = sqlDataReader.GetString("LastName"),
         FirstName = sqlDataReader.GetString("FirstName"),
         Title = sqlDataReader.GetString("Title"),
         TitleOfCourtesy = sqlDataReader.GetString("TitleOfCourtesy"),
         // We must convert DateTime to UTC DateTime and then to Timestamp.
         BirthDate = sqlDataReader.GetDateTime("BirthDate").ToUniversalTime().ToTimestamp(),
         HireDate = sqlDataReader.GetDateTime("HireDate").ToUniversalTime().ToTimestamp(),
         Address = sqlDataReader.GetString("Address"),
         City = sqlDataReader.GetString("City"),
         Region = sqlDataReader.IsDBNull("Region") ? string.Empty : sqlDataReader.GetString("Region"),
         PostalCode = sqlDataReader.GetString("PostalCode"),
         Country = sqlDataReader.GetString("Country"),
         HomePhone = sqlDataReader.GetString("HomePhone"),
         Extension = sqlDataReader.GetString("Extension"),
         // We must convert byte[] to ByteString.
         Photo = sqlDataReader.IsDBNull("Photo")
            ? ByteString.Empty
            : ByteString.FromStream(sqlDataReader.GetStream("Photo")),
         // Any nullable column must be checked for DBNull.
         Notes = sqlDataReader.IsDBNull("Notes") ? string.Empty : sqlDataReader.GetString("Notes"),
         ReportsTo = sqlDataReader.IsDBNull("ReportsTo") ? 0 : sqlDataReader.GetInt32("ReportsTo"),
         PhotoPath = sqlDataReader.IsDBNull("PhotoPath") ? string.Empty : sqlDataReader.GetString("PhotoPath")
      };

      return employeeReply;
   }

   public override async Task<EmployeeReply?> GetEmployee(EmployeeRequest request, ServerCallContext context)
   {
      var cmd = await GetCommand();
      cmd.CommandText = "SELECT * FROM Employees WHERE EmployeeId = @id";
      cmd.Parameters.AddWithValue("id", request.EmployeeId);
      var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
      EmployeeReply? employee = null;

      // Read the expected single row.
      if (await reader.ReadAsync())
      {
         employee = ReaderToEmployee(reader);
      }

      await reader.CloseAsync();
      return employee;
   }

   public override async Task<EmployeesReply?> GetEmployees(EmployeesRequest request, ServerCallContext context)
   {
      var cmd = await GetCommand();
      cmd.CommandText = "SELECT * FROM Employees";
      var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult);
      EmployeesReply employees = new();
      while (await reader.ReadAsync())
      {
         employees.Employees.Add(ReaderToEmployee(reader));
      }

      await reader.CloseAsync();
      return employees;
   }
}