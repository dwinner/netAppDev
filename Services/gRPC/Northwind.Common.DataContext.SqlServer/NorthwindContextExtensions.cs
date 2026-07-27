using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
// SqlConnectionStringBuilder
// UseSqlServer

// IServiceCollection

namespace Northwind.EntityModels;

public static class NorthwindContextExtensions
{
    /// <summary>
    ///    Adds NorthwindContext to the specified IServiceCollection. Uses the SqlServer database provider.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">Set to override the default.</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    public static IServiceCollection AddNorthwindContext(this IServiceCollection services, string? connectionString = null)
   {
      if (connectionString == null)
      {
         SqlConnectionStringBuilder builder = new();

         builder.DataSource = ".";
         builder.InitialCatalog = "Northwind";
         builder.TrustServerCertificate = true;
         builder.MultipleActiveResultSets = true;

         // If using Azure SQL Edge.
         // builder.DataSource = "tcp:127.0.0.1,1433";

         // Because we want to fail fast. Default is 15 seconds.
         builder.ConnectTimeout = 3;

         // If using Windows Integrated authentication.
         builder.IntegratedSecurity = true;

         // If using SQL Server authentication.
         // builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
         // builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");

         connectionString =
            "Data Source=DESKTOP-8DP9KDI;User ID=sa;Password=4w-.&8zRk3FCz!V;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30;Initial Catalog=Northwind";
      }

      services.AddDbContext<NorthwindContext>(options =>
         {
            options.UseSqlServer(connectionString);

            // Log to console when executing EF Core commands.
            options.LogTo(Console.WriteLine,
               new[]
               {
                  RelationalEventId.CommandExecuting
               });
         },
         // Register with a transient lifetime to avoid concurrency
         // issues with Blazor Server projects.
         ServiceLifetime.Transient,
         ServiceLifetime.Transient);

      return services;
   }
}