/**
 * Операции преобразования
 */

using System;
using System.Collections.Generic;
using System.Linq;
using DataLib;

namespace CastOperations
{
   static class Program
   {
      static void Main()
      {
         ToListSample();
         Console.WriteLine();

         ToLookupSample();
         Console.WriteLine();

         Untyped();
         Console.WriteLine();
      }

      private static void ToListSample()  // Преобразование к списку
      {
         var racers = (from racer in Formula1.Champions
                       where racer.Starts > 150
                       orderby racer.Starts descending
                       select racer).ToList();

         racers.ForEach(racer => Console.WriteLine(racer.FirstName));
      }

      private static void ToLookupSample()   // Группирование в "мульти"-словарь
      {
         ILookup<string, Racer> racers = (from racer in Formula1.Champions
                                          from car in racer.Cars
                                          select new
                                          {
                                             Car = car,
                                             Racer = racer
                                          }).ToLookup(crArg => crArg.Car, crArg => crArg.Racer);

         if (racers.Contains("Williams"))
         {
            foreach (var williamsRacer in racers["Williams"])
            {
               Console.WriteLine(williamsRacer);
            }
         }
      }

      private static void Untyped() // Преобразование в строготипизированный запрос
      {
         var list = new System.Collections.ArrayList(Formula1.Champions as System.Collections.ICollection);

         var query = from r in list.Cast<Racer>()
                     where r.Country == "USA"
                     orderby r.Wins descending
                     select r;
         foreach (var racer in query)
         {
            Console.WriteLine("{0:A}", racer);
         }
      }
   }
}
