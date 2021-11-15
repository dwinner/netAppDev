/**
 * Using JSON-configuration
 */

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UsersDbEntities;

namespace UsingJsonConfiguration
{
   internal static class Program
   {
      private static readonly string ConnectionString;

      static Program()
      {
         var builder = new ConfigurationBuilder();
         builder.SetBasePath(Directory.GetCurrentDirectory());
         builder.AddJsonFile("appsettings.json");
         var config = builder.Build();
         ConnectionString = config.GetConnectionString("DefaultConnection");
      }

      private static async Task Main()
      {
         var optionsBuilder = new DbContextOptionsBuilder<UsersContext>();
         var options = optionsBuilder.UseSqlServer(ConnectionString).Options;

         await using var context = new UsersContext(options);
         var users = await context.Users.ToListAsync().ConfigureAwait(false);
         users.ForEach(user => Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}"));

         Console.Read();
      }
   }
}