/**
 * Конкатенация элементов коллекции в одну строку
 */

using System;
using System.Linq;

namespace StringConcat
{
   class Program
   {
      static void Main()
      {
         // Стандартная конкатенация элементов
         int[] vals = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
         Console.WriteLine(string.Concat(vals));

         // Конкатенация с разделителями
         Console.WriteLine(string.Join(", ", vals));

         // Конкатенация объектов пользовательских типов
         Person[] persons = new[]
            {
               new Person { FirstName = "Bill", LastName = "Gates" },
               new Person { FirstName = "Steve", LastName = "Ballmer" },
               new Person { FirstName = "Steve", LastName = "Jobs" }
            };
         Console.WriteLine(string.Join<Person>(", ", persons));   // Не то, что нам нужно
         // То, что нам нужно
         Console.WriteLine(persons.Aggregate(string.Empty, (s, person) => s.Length > 0 ? s + ", " + person.LastName : person.LastName));

         Console.ReadKey();
      }
   }
}
