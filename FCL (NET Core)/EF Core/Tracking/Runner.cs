using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

internal class Runner
{
   private readonly IDbContextFactory<MenusContext> _menusContextFactory;

   public Runner(IDbContextFactory<MenusContext> menusContextFactory) =>
      _menusContextFactory = menusContextFactory;

   public async Task CreateDatabaseAsync()
   {
      await using var context = _menusContextFactory.CreateDbContext();
      await context.Database.EnsureCreatedAsync();
   }

   public async Task AddRecordsAsync()
   {
      Console.WriteLine(nameof(AddRecordsAsync));
      await using var context = _menusContextFactory.CreateDbContext();
      MenuCard soupCard = new("Soups");

      MenuItem[] soups =
      {
         new("Consommé Célestine (with shredded pancake)")
         {
            Price = 4.8m,
            MenuCard = soupCard
         },
         new("Baked Potato Soup")
         {
            Price = 4.8m,
            MenuCard = soupCard
         },
         new("Cheddar Broccoli Soup")
         {
            Price = 4.8m,
            MenuCard = soupCard
         }
      };

      foreach (var soup in soups)
      {
         soupCard.MenuItems.Add(soup);
      }

      context.MenuCards.Add(soupCard);

      ShowState(context);
      var records = await context.SaveChangesAsync();
      Console.WriteLine($"{records} added");
      Console.WriteLine();
   }

   private static void ShowState(DbContext context)
   {
      foreach (EntityEntry entry in context.ChangeTracker.Entries())
      {
         Console.WriteLine($"type: {entry.Entity.GetType().Name}, " +
                           $"state: {entry.State}, {entry.Entity}");
      }

      Console.WriteLine();
   }

   public async Task ObjectTrackingAsync()
   {
      await using var context = _menusContextFactory.CreateDbContext();
      Console.WriteLine(nameof(ObjectTrackingAsync));
      var m1 = await (from m in context.MenusItems
         where m.Text.StartsWith("Con")
         select m).FirstOrDefaultAsync();
      var m2 = await (from m in context.MenusItems
         where m.Text.Contains("(")
         select m).FirstOrDefaultAsync();
      Console.WriteLine(ReferenceEquals(m1, m2) ? "the same object" : "not the same");

      ShowState(context);

      Console.WriteLine();
   }

   public async Task UpdateRecordsAsync()
   {
      await using var context = _menusContextFactory.CreateDbContext();
      MenuItem menu = await context.MenusItems
         .Skip(1)
         .FirstOrDefaultAsync();

      ShowState(context);
      menu.Price += 0.2m;
      ShowState(context);
      var records = await context.SaveChangesAsync();
      Console.WriteLine($"{records} updated");
      ShowState(context);
   }

   public async Task UpdateRecordUntrackedAsync()
   {
      async Task<MenuItem> GetMenuItemAsync()
      {
         await using var context = _menusContextFactory.CreateDbContext();
         return await context.MenusItems
            .Skip(2)
            .FirstAsync();
      }

      async Task UpdateMenuItemAsync(MenuItem menu)
      {
         await using var context = _menusContextFactory.CreateDbContext();
         ShowState(context);
         // EntityEntry<Menu> entry = context.Menus.Attach(m);
         // entry.State = EntityState.Modified;
         context.MenusItems.Update(menu);
         ShowState(context);
         await context.SaveChangesAsync();
      }

      var menu = await GetMenuItemAsync();
      menu.Price += 0.7m;
      await UpdateMenuItemAsync(menu).ConfigureAwait(false);
   }

   public async Task DeleteDatabaseAsync()
   {
      await using var context = _menusContextFactory.CreateDbContext();
      Console.Write("Delete the database? (y|n) ");
      var input = Console.ReadLine();
      if (input?.ToLower() == "y")
      {
         var deleted = await context.Database.EnsureDeletedAsync();
         string deletionInfo = deleted ? "deleted" : "not deleted";
         Console.WriteLine($"database {deletionInfo}");
      }
   }
}