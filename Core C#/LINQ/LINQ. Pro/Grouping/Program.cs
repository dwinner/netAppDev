/**
 * Группировка
 */

using System;
using System.Linq;
using DataLib;

namespace Grouping
{
   static class Program
   {
      static void Main()
      {
         Grouping();
         Console.WriteLine();

         GroupingWithNestedObjects();
         Console.WriteLine();
      }

      private static void Grouping()   // Простая группировка
      {
         var countries = from racer in Formula1.Champions
                         group racer by racer.Country
                            into racersByCountry
                            orderby racersByCountry.Count() descending, racersByCountry.Key
                            where racersByCountry.Count() > 2
                            select new
                            {
                               Country = racersByCountry.Key,
                               Count = racersByCountry.Count()
                            };
         foreach (var item in countries)
         {
            Console.WriteLine("{0, -10} {1}", item.Country, item.Count);
         }
      }

      private static void GroupingWithNestedObjects() // Группировка с вложенными объектами
      {
         var countryRacers = from racer in Formula1.Champions
                             group racer by racer.Country
                                into g
                                orderby g.Count() descending, g.Key
                                where g.Count() >= 2
                                select new
                                {
                                   Country = g.Key,
                                   Count = g.Count(),
                                   Racers = from r in g
                                            orderby r.LastName
                                            select string.Format("{0} {1}", r.FirstName, r.LastName)
                                };
         foreach (var item in countryRacers)
         {
            Console.WriteLine("{0, -10} {1}", item.Country, item.Count);
            foreach (var racerName in item.Racers)
            {
               Console.Write("{0}; ", racerName);
            }
            Console.WriteLine();
         }
      }
   }
}
