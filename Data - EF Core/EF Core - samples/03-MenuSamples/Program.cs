using System;
using System.Diagnostics;
using System.Linq;
using _03_MenuSamples;
using Microsoft.EntityFrameworkCore;

CreateDatabase();
AddRecords();
ObjectTracking();
UpdateRecords();
ChangeUntracked();
AddHundredRecords();
DeleteDatabase();

static void AddHundredRecords()
{
   Console.WriteLine(nameof(AddHundredRecords));
   using var context = new MenusContext();
   var card = context.MenuCards.FirstOrDefault();
   if (card != null)
   {
      var menus = Enumerable.Range(1, 100).Select(x => new Menu
      {
         MenuCard = card,
         Text = $"menu {x}",
         Price = 9.9m
      });
      context.Menus.AddRange(menus);
      var stopwatch = Stopwatch.StartNew();
      var records = context.SaveChanges();
      stopwatch.Stop();
      Console.WriteLine($"{records} records added after {stopwatch.ElapsedMilliseconds} milliseconds");
   }

   Console.WriteLine();
}

static void AddRecords()
{
   Console.WriteLine(nameof(AddRecords));
   try
   {
      using var context = new MenusContext();
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

      var records = context.SaveChanges();
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

static void ObjectTracking()
{
   Console.WriteLine(nameof(ObjectTracking));
   using var context = new MenusContext();
   var m1 = (from m in context.Menus
      where m.Text.StartsWith("Con")
      select m).FirstOrDefault();
   var m2 = (from m in context.Menus
      where m.Text.Contains("(")
      select m).FirstOrDefault();
   Console.WriteLine(ReferenceEquals(m1, m2) ? "the same object" : "not the same");

   ShowState(context);

   Console.WriteLine();
}

static void UpdateRecords()
{
   Console.WriteLine(nameof(UpdateRecords));
   using var context = new MenusContext();
   var menu = context.Menus
      .Skip(1)
      .FirstOrDefault();
   ShowState(context);
   menu.Price += 0.2m;
   ShowState(context);
   var records = context.SaveChanges();
   Console.WriteLine($"{records} updated");
   ShowState(context);

   Console.WriteLine();
}

static void ChangeUntracked()
{
   Console.WriteLine(nameof(ChangeUntracked));

   static Menu GetMenu()
   {
      using var context = new MenusContext();
      var menu = context.Menus
         .Skip(2)
         .FirstOrDefault();
      return menu;
   }

   var m = GetMenu();
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

static void CreateDatabase()
{
   using var context = new MenusContext();
   var created = context.Database.EnsureCreated();
   Console.WriteLine(created);
}

static void DeleteDatabase()
{
   Console.Write("Delete the database? ");
   var input = Console.ReadLine();
   if (input?.ToLower() == "y")
   {
      using var context = new MenusContext();
      var deleted = context.Database.EnsureDeleted();
      Console.WriteLine(deleted);
   }
}