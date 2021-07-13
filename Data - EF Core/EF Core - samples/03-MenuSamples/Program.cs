using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using _03_MenuSamples;
using Microsoft.EntityFrameworkCore;

await CreateDatabase().ConfigureAwait(false);
await AddRecords().ConfigureAwait(false);
await ObjectTracking().ConfigureAwait(false);
await UpdateRecords().ConfigureAwait(false);
await ChangeUntracked().ConfigureAwait(false);
await AddHundredRecords().ConfigureAwait(false);
await DeleteDatabase().ConfigureAwait(false);

static async Task AddHundredRecords()
{
   Console.WriteLine(nameof(AddHundredRecords));
   await using var context = new MenusContext();
   var card = await context.MenuCards.FirstOrDefaultAsync().ConfigureAwait(false);
   if (card != null)
   {
      var menus = Enumerable.Range(1, 100).Select(x => new Menu
      {
         MenuCard = card,
         Text = $"menu {x}",
         Price = 9.9m
      });

      await context.Menus.AddRangeAsync(menus).ConfigureAwait(false);
      var stopwatch = Stopwatch.StartNew();
      var records = await context.SaveChangesAsync().ConfigureAwait(false);
      stopwatch.Stop();
      Console.WriteLine($"{records} records added after {stopwatch.ElapsedMilliseconds} milliseconds");
   }

   Console.WriteLine();
}

static async Task AddRecords()
{
   Console.WriteLine(nameof(AddRecords));
   try
   {
      await using var context = new MenusContext();
      var soupCard = new MenuCard();
      Menu[] soups =
      {
         new()
         {
            Text = "Consommé Célestine (with shredded pancake)",
            Price = 4.8m,
            MenuCard = soupCard
         },
         new()
         {
            Text = "Baked Potato Soup",
            Price = 4.8m,
            MenuCard = soupCard
         },
         new()
         {
            Text = "Cheddar Broccoli Soup",
            Price = 4.8m,
            MenuCard = soupCard
         }
      };

      soupCard.Title = "Soups";
      soupCard.Menus.AddRange(soups);
      context.MenuCards.Add(soupCard);

      ShowState(context);

      var records = await context.SaveChangesAsync().ConfigureAwait(false);
      Console.WriteLine($"{records} added");
      Console.WriteLine();
   }
   catch (Exception ex)
   {
      Console.WriteLine(ex.Message);
   }

   Console.WriteLine();
}

static void ShowState(DbContext context)
{
   foreach (var entry in context.ChangeTracker.Entries())
   {
      Console.WriteLine($"type: {entry.Entity.GetType().Name}, state: {entry.State}," +
                        $" {entry.Entity}");
   }
}

static async Task ObjectTracking()
{
   Console.WriteLine(nameof(ObjectTracking));
   await using var context = new MenusContext();
   var m1 = await
      (from m in context.Menus
         where m.Text.StartsWith("Con")
         select m).FirstOrDefaultAsync().ConfigureAwait(false);
   var m2 = await
      (from m in context.Menus
         where m.Text.Contains("(")
         select m).FirstOrDefaultAsync().ConfigureAwait(false);

   Console.WriteLine(ReferenceEquals(m1, m2) ? "the same object" : "not the same");

   ShowState(context);

   Console.WriteLine();
}

static async Task UpdateRecords()
{
   Console.WriteLine(nameof(UpdateRecords));
   await using var context = new MenusContext();
   var menu = await context.Menus
      .Skip(1)
      .FirstOrDefaultAsync().ConfigureAwait(false);

   ShowState(context);

   menu.Price += 0.2m;

   ShowState(context);

   var records = await context.SaveChangesAsync().ConfigureAwait(false);
   Console.WriteLine($"{records} updated");
   ShowState(context);

   Console.WriteLine();
}

static async Task ChangeUntracked()
{
   Console.WriteLine(nameof(ChangeUntracked));

   static async Task<Menu> GetMenu()
   {
      await using var context = new MenusContext();
      var menu = await context.Menus
         .Skip(2)
         .FirstOrDefaultAsync().ConfigureAwait(false);

      return menu;
   }

   var m = await GetMenu().ConfigureAwait(false);
   m.Price += 0.7m;
   UpdateUntracked(m);
}

static void UpdateUntracked(Menu m)
{
   using var context = new MenusContext();
   ShowState(context);
   // EntityEntry<Menu> entry = context.Menus.Attach(m);
   // entry.State = EntityState.Modified;
   context.Menus.Update(m);
   ShowState(context);
   context.SaveChanges();
}

static async Task CreateDatabase()
{
   await using var context = new MenusContext();
   var created = await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
   Console.WriteLine(created);
}

static async Task DeleteDatabase()
{
   Console.Write("Delete the database? ");
   var input = Console.ReadLine();
   if (input?.ToLower() == "y")
   {
      await using var context = new MenusContext();
      var deleted = await context.Database.EnsureDeletedAsync().ConfigureAwait(false);
      Console.WriteLine(deleted);
   }
}