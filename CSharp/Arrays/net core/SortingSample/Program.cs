using System;

namespace Wrox.ProCSharp.Arrays
{
   internal static class Program
   {
      private static void Main()
      {
         SortNames();
         Console.WriteLine();
         Person[] persons = GetPersons();
         SortPersons(persons);
         Console.WriteLine();
         SortUsingPersonComparer(persons);
      }

      private static void SortUsingPersonComparer(Person[] persons)
      {
         Array.Sort(persons, new PersonComparer(PersonCompareType.FirstName));
         Array.ForEach(persons, Console.WriteLine);
      }

      private static Person[] GetPersons()
      {
         return new Person[]
         {
            new("Damon", "Hill"),
            new("Niki", "Lauda"),
            new("Ayrton", "Senna"),
            new("Graham", "Hill")
         };
      }

      private static void SortPersons(Person[] persons)
      {
         Array.Sort(persons);
         Array.ForEach(persons, Console.WriteLine);
         foreach (Person p in persons)
         {
            Console.WriteLine(p);
         }
      }

      private static void SortNames()
      {
         string[] names =
         {
            "Lady Gaga",
            "Shakira",
            "Beyonce",
            "Ava Max"
         };

         Array.Sort(names);
         Array.ForEach(names, Console.WriteLine);
      }
   }
}