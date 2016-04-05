/**
 * Сортировка массивов
 */

using System;

namespace SortingSample
{
   static class Program
   {
      static void Main()
      {
         Console.WriteLine("Типы, поддерживающее встроенное сравнение");

         // Note: Типы, поддерживающие сравнение
         string[] names = { "Christina Aguilera", "Shakira", "Beyonce", "Gwen Stefani" };
         Array.Sort(names);
         Array.ForEach(names, Console.WriteLine);

         Console.WriteLine("Соритровка типов, реализующих интерфейс IComparable<T>");

         // Note: Сортировка типов, реализующих интерфейс IComparable<T>
         Person[] persons = GetPersons();
         Array.Sort(persons);
         Array.ForEach(persons, Console.WriteLine);

         Console.WriteLine("Сортировка типов через заданный компаратор");

         // Note: Сортировка типов через заданный компаратор
         Person[] otherPersons = GetPersons();
         Array.Sort(otherPersons, PersonComparerPool.CreateComparerType(PersonCompareType.FirstName));
         Array.ForEach(otherPersons, Console.WriteLine);

         Console.WriteLine("Сортировка функциональным образом");

         // Note: Сортировка функциональным образом
         Person[] anotherPersons = GetPersons();
         Array.Sort(anotherPersons,
            (firstPerson, secondPerson) =>
               string.Compare(firstPerson.LastName, secondPerson.LastName, StringComparison.InvariantCulture));
         Array.ForEach(anotherPersons, Console.WriteLine);
      }

      static Person[] GetPersons()
      {
         return new[]
         {
            new Person { FirstName="Damon", LastName="Hill" },
            new Person { FirstName="Niki", LastName="Lauda" },
            new Person { FirstName="Ayrton", LastName="Senna" },
            new Person { FirstName="Graham", LastName="Hill" }
         };
      }
   }
}
