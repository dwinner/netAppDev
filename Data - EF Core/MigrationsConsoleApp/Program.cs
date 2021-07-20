using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MigrationsLib;

namespace MigrationsConsoleApp
{
   internal class Program
   {
      private const string ConnectionString =
         @"server=(localdb)\mssqllocaldb;database=ProCSharpMigrations;trusted_connection=true";

      public static IServiceProvider Container { get; private set; }

      private static void Main(string[] args)
      {
         RegisterServices();
         var context = Container.GetService<MenusContext>();
         context.Database.Migrate();
      }

      private static void RegisterServices()
      {
         var services = new ServiceCollection();
         services.AddDbContext<MenusContext>(options =>
            options.UseSqlServer(ConnectionString));
         Container = services.BuildServiceProvider();
      }
   }
}