/**
 * Аггрегирующие операции
 */

using System;
using System.Linq;
using DataLib;

namespace AggregateOperations
{
   static class Program
   {
      static void Main()
      {
         Aggregate();
         Console.WriteLine();

         Aggregate2();
         Console.WriteLine();
      }

      private static void Aggregate()
      {
         var query = from racer in Formula1.Champions
                     let numberYears = racer.Years.Count()
                     where numberYears >= 3
                     orderby numberYears descending, racer.LastName
                     select new
                     {
                        Name = string.Format("{0} {1}", racer.FirstName, racer.LastName),
                        TimesChampion = numberYears
                     };

         foreach (var r in query)
         {
            Console.WriteLine("{0} {1}", r.Name, r.TimesChampion);
         }
      }

      private static void Aggregate2()
      {
         var countries = (from c in
                             from racer in Formula1.Champions
                             group racer by racer.Country
                                into rByc
                                select new
                                {
                                   Country = rByc.Key,
                                   Wins = (from r in rByc select r.Wins).Sum()
                                }
                          orderby c.Wins descending, c.Country
                          select c).Take(5);

         foreach (var country in countries)
         {
            Console.WriteLine("{0} {1}", country.Country, country.Wins);
         }
      }
   }
}
