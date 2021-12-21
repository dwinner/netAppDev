/*
 * Simple example for EF Core
 */

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HelloEFCore
{
   internal static class Program
   {
      private static async Task Main()
      {
         await using var db = new ApplicationContext();

         // create 2 user objects
         var user1 = new User {Name = "Tom", Age = 33};
         var user2 = new User {Name = "Alice", Age = 26};

         // Add users to database
         await db.Users.AddAsync(user1).ConfigureAwait(false);
         await db.Users.AddAsync(user2).ConfigureAwait(false);
         await db.SaveChangesAsync().ConfigureAwait(false);

         Console.WriteLine("Changes have been saved");

         // Get objects from DB and output them
         var users = await db.Users.ToListAsync().ConfigureAwait(false);
         users.ForEach(user => Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}"));
      }
   }
}