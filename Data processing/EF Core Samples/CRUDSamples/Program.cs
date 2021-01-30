/**
 * CRUD operations
 */

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using UsersDbEntities;

namespace CRUDSamples
{
   internal static class Program
   {
      private static async Task Main()
      {
         // Adding
         await using (var context = new UsersContext())
         {
            // Use custom logger
            context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());

            var user1 = new User {Name = "Tom", Age = 33};
            var user2 = new User {Name = "Alice", Age = 26};

            // Add new users
            await context.AddRangeAsync(user1, user2).ConfigureAwait(false);
            //await context.Users.AddAsync(user1).ConfigureAwait(false);
            //await context.Users.AddAsync(user2).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
         }

         // Getting users
         await using (var context = new UsersContext())
         {
            var users = await context.Users.ToListAsync().ConfigureAwait(false);
            Console.WriteLine("Data after adding:");
            users.ForEach(user => Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}"));
         }

         // Updating
         await using (var context = new UsersContext())
         {
            // Get the 1st object
            var user = await context.Users.FirstOrDefaultAsync().ConfigureAwait(false);
            if (user != null)
            {
               user.Name = "Bob";
               user.Age = 44;

               context.Users.Update(user); // if you get the object at out of context

               // Update the changes
               await context.SaveChangesAsync().ConfigureAwait(false);
            }

            // Output updating data
            Console.WriteLine("\nData after updating:");
            var users = await context.Users.ToListAsync().ConfigureAwait(false);
            users.ForEach(usr => Console.WriteLine($"{usr.Id}.{usr.Name} - {usr.Age}"));
         }

         // Deleting
         await using (var context = new UsersContext())
         {
            // Get the 1st object
            var user = await context.Users.FirstOrDefaultAsync().ConfigureAwait(false);
            if (user != null)
            {
               // Deleting object
               context.Users.Remove(user);
               await context.SaveChangesAsync().ConfigureAwait(false);
            }

            // Output updating data
            Console.WriteLine("\nData after removing:");
            var users = context.Users.ToList();
            users.ForEach(usr => Console.WriteLine($"{usr.Id}.{usr.Name} - {usr.Age}"));
         }

         Console.Read();
      }
   }
}