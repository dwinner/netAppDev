using System;
using System.Collections.Generic;
using System.Threading;

namespace WeakReferenceCache
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("HybridCache");
         var cache = new HybridCache<int, string>(TimeSpan.FromSeconds(5));

         cache.Add(1, "One");
         cache.Add(2, "Two");
         Thread.Sleep(5000);

         // Read a value
         var success = cache.TryGetValue(1, out var val);
         Console.WriteLine(val);
         Console.WriteLine($"Success: {success}");

         GC.Collect();
         GC.WaitForFullGCComplete();

         // Try reading again
         success = cache.TryGetValue(1, out val);
         Console.WriteLine(val);
         Console.WriteLine($"Success: {success}");
         Console.WriteLine();
         Console.WriteLine();

         Console.WriteLine("Multi-index");

         var p1 = new Person
         {
            Id = "1", 
            FirstName = "Bob", 
            LastName = "Jones", 
            Birthday = new DateTime(1980, 1, 1)
         };
         var p2 = new Person
         {
            Id = "2", 
            FirstName = "Alice", 
            LastName = "Smith", 
            Birthday = new DateTime(1980, 1, 1)
         };
         var p3 = new Person
         {
            Id = "3", 
            FirstName = "Eve", 
            LastName = "Brown", 
            Birthday = new DateTime(1978, 8, 1)
         };

         var personDb = new PersonDatabase();
         personDb.AddPerson(p1);
         personDb.AddPerson(p2);
         personDb.AddPerson(p3);

         // get rid of our strong references
         p1 = p2 = p3 = null;

         var targetBirthday = new DateTime(1980, 1, 1);

         PrintBirthdays(personDb, targetBirthday);

         personDb.RemovePerson("2");

         PrintBirthdays(personDb, targetBirthday);

         GC.Collect();
         Console.WriteLine("After GC");
         PrintBirthdays(personDb, targetBirthday);
      }

      private static void PrintBirthdays(PersonDatabase db, DateTime targetBirthday)
      {
         Console.WriteLine("Birthdays on Jan 1, 1980:");
         db.TryGetByBirthday(targetBirthday, out var people);
         foreach (var person in people)
         {
            Console.Write("\t");
            Console.WriteLine(person.FirstName);
         }

         Console.WriteLine($"Indexes need rebuilt? {db.NeedsIndexRebuild}");
         Console.WriteLine();
      }
   }
}