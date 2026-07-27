using System.Data;
using Grpc.Core;
using Microsoft.Data.SqlClient;

// To use ServerCallContext.
// To use SqlConnection and so on.

// To use CommandType.

namespace Northwind.Grpc.Service.Services;

public class ProductService(ILogger<ProductService> logger) : Product.ProductBase
{
   private readonly ILogger<ProductService> _logger = logger;

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
      builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
      builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");
      builder.PersistSecurityInfo = false;
      */

      SqlConnection connection = new(builder.ConnectionString);

      await connection.OpenAsync();

      var cmd = connection.CreateCommand();
      cmd.CommandType = CommandType.Text;
      return cmd;
   }

   private ProductReply ReaderToProduct(SqlDataReader sqlDataReader)
   {
      ProductReply productReply = new()
      {
         ProductId = sqlDataReader.GetInt32("ProductId"),
         ProductName = sqlDataReader.GetString("ProductName"),
         SupplierId = sqlDataReader.IsDBNull("SupplierId") ? 0 : sqlDataReader.GetInt32("SupplierId"),
         CategoryId = sqlDataReader.IsDBNull("CategoryId") ? 0 : sqlDataReader.GetInt32("CategoryId"),
         QuantityPerUnit = sqlDataReader.GetString("QuantityPerUnit"),
         // Uses our custom conversion from decimal to DecimalValue.
         UnitPrice = sqlDataReader.IsDBNull("UnitPrice") ? 0M : sqlDataReader.GetDecimal("UnitPrice"),
         UnitsInStock = sqlDataReader.IsDBNull("UnitsInStock") ? 0 : sqlDataReader.GetInt16("UnitsInStock"),
         UnitsOnOrder = sqlDataReader.IsDBNull("UnitsOnOrder") ? 0 : sqlDataReader.GetInt16("UnitsOnOrder"),
         ReorderLevel = sqlDataReader.IsDBNull("ReorderLevel") ? 0 : sqlDataReader.GetInt16("ReorderLevel"),
         Discontinued = !sqlDataReader.IsDBNull("Discontinued") && sqlDataReader.GetBoolean("Discontinued")
      };

      return productReply;
   }

   public override async Task<ProductReply?> GetProduct(ProductRequest request, ServerCallContext context)
   {
      var cmd = await GetCommand();
      cmd.CommandText = "SELECT * FROM Products WHERE ProductId = @id";
      cmd.Parameters.AddWithValue("id", request.ProductId);
      var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
      ProductReply? product = null;

      // Read the expected single row.
      if (await reader.ReadAsync())
      {
         product = ReaderToProduct(reader);
      }

      await reader.CloseAsync();

      return product;
   }

   public override async Task<ProductsReply?> GetProducts(ProductsRequest request, ServerCallContext context)
   {
      var cmd = await GetCommand();
      cmd.CommandText = "SELECT * FROM Products";
      var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult);
      ProductsReply products = new();
      while (await reader.ReadAsync())
      {
         products.Products.Add(ReaderToProduct(reader));
      }

      await reader.CloseAsync();
      return products;
   }

   public override async Task<ProductsReply?> GetProductsMinimumPrice(
      ProductsMinimumPriceRequest request,
      ServerCallContext context)
   {
      var cmd = await GetCommand();
      cmd.CommandText = "SELECT * FROM Products WHERE UnitPrice >= @price";

      // We must cast DecimalValue to a decimal value because SqlClient
      // does not understand what to do with a DecimalValue.
      cmd.Parameters.AddWithValue("price", (decimal)request.MinimumPrice);
      var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult);
      ProductsReply products = new();
      while (await reader.ReadAsync())
      {
         products.Products.Add(ReaderToProduct(reader));
      }

      await reader.CloseAsync();

      return products;
   }
}