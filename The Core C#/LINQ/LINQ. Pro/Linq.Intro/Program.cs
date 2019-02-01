/**
 * Простые принципы использования LINQ
 */

using System;
using System.Collections.Generic;
using System.Linq;
using DataLib;

namespace Linq.Intro
{
   static class Program
   {
      static void Main()
      {
         DeferredQuery();
         Console.WriteLine();

         LinqQuery();
         Console.WriteLine();

         ExtensionMethods();
         Console.WriteLine();
      }

      static void ExtensionMethods()   // Методы расширения
      {
         var champions = new List<Racer>(Formula1.Champions);
         IEnumerable<Racer> brazilChampions =
            champions.Where(racer => racer.Country == "Brazil")
               .OrderByDescending(racer => racer.Wins)
               .Select(racer => racer);

         foreach (var racer in brazilChampions)
         {
            Console.WriteLine("{0:A}", racer);
         }
      }

      static void LinqQuery() // Простой запрос LINQ
      {
         var query = from racer in Formula1.Champions
                     where racer.Country == "Brazil"
                     orderby racer.Wins descending
                     select racer;

         foreach (var racer in query)
         {
            Console.WriteLine(racer);
         }
      }

      static void DeferredQuery()   // Отложенное выполнение запроса
      {
         var names = new List<string> { "Nino", "Alberto", "Juan", "Mike", "Phil" };

         var namesWithJ = from name in names
                          where name.StartsWith("J")
                          orderby name
                          select name;

         Console.WriteLine("First iteration");
         foreach (var name in namesWithJ)
         {
            Console.WriteLine(name);
         }
         Console.WriteLine();

         names.AddRange(new[] { "John", "Jim", "Jack", "Denny" });

         Console.WriteLine("Second iteration");
         foreach (var name in namesWithJ)
         {
            Console.WriteLine(name);
         }
      }
   }
}
