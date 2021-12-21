using System;

namespace _13_OwnedEntities
{
   internal static class Program
   {
      private static void Main()
      {
         CreateDatabase();
         AddData();
         QueryPerson();
         DeleteDatabase();
      }

      private static void QueryPerson()
      {
         using var context = new OwnedEntitiesContext();
         foreach (var p in context.People)
         {
            Console.WriteLine(p.Name);
            Console.WriteLine($"Company address: {p.CompanyAddress.LineOne} {p.CompanyAddress.Location.City}");
            Console.WriteLine($"Private address: {p.PrivateAddress.LineOne} {p.PrivateAddress.Location.City}");
         }
      }

      private static void AddData()
      {
         using var context = new OwnedEntitiesContext();
         var p1 = new Person
         {
            Name = "Tom Turbo",
            CompanyAddress = new Address
            {
               LineOne = "Riesenradplatz",
               Location = new Location
               {
                  City = "Vienna",
                  Country = "Austria"
               }
            },
            PrivateAddress = new Address
            {
               LineOne = "Tiergarten Schönbrunn",
               LineTwo = "Seckendorff-Gudent-Weg",
               Location = new Location
               {
                  City = "Vienna",
                  Country = "Austria"
               }
            }
         };
         context.People.Add(p1);
         var records = context.SaveChanges();

         Console.WriteLine($"Records: {records:D}");
      }

      private static void CreateDatabase()
      {
         using var context = new OwnedEntitiesContext();
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
            using var context = new OwnedEntitiesContext();
            var deleted = context.Database.EnsureDeleted();
            var deletionInfo = deleted ? "deleted" : "not deleted";
            Console.WriteLine($"database {deletionInfo}");
         }
      }
   }
}