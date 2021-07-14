/**
 * Reverse Engineering for exixting database:
 * 1. Tools –> NuGet Package Manager –> Package Manager Console
 * 2. Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer
 */

using System;
using System.Linq;
using UsersDbEntities;

namespace ConnectToExisting
{
   internal static class Program
   {
      private static void Main()
      {
         using var db = new UsersContext();

         // Get objects and output
         var users = db.Users.ToList();
         Console.WriteLine("Object list:");
         foreach (var u in users)
         {
            Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
         }

         Console.ReadKey();
      }
   }
}