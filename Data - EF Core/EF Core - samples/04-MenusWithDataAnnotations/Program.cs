using System;
using _04_MenusWithDataAnnotations;

CreateDatabase();

static void CreateDatabase()
{
   using var context = new MenusContext();
   var created = context.Database.EnsureCreated();
   Console.WriteLine(created);
}