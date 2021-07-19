using System;
using System.Diagnostics;

namespace _12_TableSplitting
{
   internal static class Program
   {
      private static void Main()
      {
         CreateDatabase();

         using (var dbContext = new MenusContext())
         {
            var mainMenu = new Menu
            {
               Title = "Shurpa",
               Subtitle = "Lamb shurpa",
               Price = 240M
            };

            var menuDetail = new MenuDetails
            {
               KitchenInfo = "Uzbek cuisine",
               MenusSold = 12
            };

            mainMenu.Details = menuDetail;
            menuDetail.Menu = mainMenu;

            dbContext.AddRange(mainMenu, menuDetail);
            var affected = dbContext.SaveChanges();
            Debug.Assert(affected > 0);
         }

         DeleteDatabase();
      }

      private static void CreateDatabase()
      {
         using var context = new MenusContext();
         var created = context.Database.EnsureCreated();
         var creationInfo = created ? "created" : "exists";
         Console.WriteLine($"database {creationInfo}");
      }

      private static void DeleteDatabase()
      {
         Console.Write("Delete the database? ");
         var input = Console.ReadLine();
         if (input?.ToLower() == "y")
         {
            using var context = new MenusContext();
            var deleted = context.Database.EnsureDeleted();
            var deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
         }
      }
   }
}