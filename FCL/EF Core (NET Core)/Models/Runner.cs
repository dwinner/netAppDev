using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static ColumnNames;

internal class Runner
{
   private readonly MenusContext _menusContext;

   public Runner(MenusContext menusContext) => _menusContext = menusContext;

   public Task CreateDatabaseAsync() => _menusContext.Database.EnsureCreatedAsync();

   public async Task DeleteMenuItemsAsync(int id)
   {
      var menuItem = await _menusContext.MenuItems.FindAsync(id);
      if (menuItem is null)
      {
         return;
      }

      _menusContext.Remove(menuItem);
      var records = await _menusContext.SaveChangesAsync();
      Console.WriteLine($"{records} deleted");
   }

   public async Task QueryDeletedMenuItemsAsync()
   {
      IEnumerable<MenuItem> deletedMenuItems =
         await _menusContext.MenuItems
            .Where(b => EF.Property<bool>(b, IsDeleted))
            .ToListAsync();

      foreach (var menu in deletedMenuItems)
      {
         Console.WriteLine($"deleted: {menu}");
      }
   }

   public async Task DeleteDatabaseAsync()
   {
      Console.Write("Delete the database? (y|n) ");
      var input = Console.ReadLine();
      if (input?.ToLower() == "y")
      {
         var deleted = await _menusContext.Database.EnsureDeletedAsync();
         string deletionInfo = deleted ? "deleted" : "not deleted";
         Console.WriteLine($"database {deletionInfo}");
      }
   }
}