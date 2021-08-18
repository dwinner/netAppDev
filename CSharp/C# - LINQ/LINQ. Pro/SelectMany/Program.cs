/**
 * Составные операции выборки
 */

using System;
using System.Collections.Generic;
using System.Linq;
using DataLib;

namespace SelectMany
{
   public static class Program
   {
      public static void Main()
      {
         CompoundFrom();
         Console.WriteLine();

         SelectManyExt();
         Console.WriteLine();

         SelectMany2();
         Console.WriteLine();

         CombineRacers();
         Console.WriteLine();
      }

      static void CompoundFrom() // Составная конструкция from
      {
         var ferrariDrivers = from racer in Formula1.Champions
                              from car in racer.Cars
                              where car == "Ferrari"
                              orderby racer.LastName
                              select string.Format("{0} {1}", racer.FirstName, racer.LastName);
         foreach (var driver in ferrariDrivers)
         {
            Console.WriteLine(driver);
         }
      }

      static void SelectManyExt()   // Расширяющий метод SelectMany
      {
         var ferrariDrivers = Formula1.Champions.SelectMany(racer => racer.Cars, (racer, car) => new { Racer = racer, Car = car })
            .Where(racer => racer.Car == "Ferrari")
            .OrderBy(racer => racer.Racer.LastName)
            .Select(racer => string.Format("{0} {1}", racer.Racer.FirstName, racer.Racer.LastName));
         foreach (var driver in ferrariDrivers)
         {
            Console.WriteLine(driver);
         }
      }

      private static void SelectMany2()    // Разглаживание последовательности
      {
         var racers = Formula1.Championships.SelectMany(cs => new List<dynamic>
            {
               new
               {
                  cs.Year,
                  Position = 1,
                  Name = cs.First
               },
               new
               {
                  cs.Year,
                  Position = 2,
                  Name = cs.Second
               },
               new
               {
                  cs.Year,
                  Position = 3,
                  Name = cs.Third
               }
            });


         foreach (var s in racers)
         {
            Console.WriteLine(s);
         }
      }

      private static void CombineRacers() // Комбинирование последовательностей при группировке
      {
         var q = from r in Formula1.Champions
                 join r2 in Formula1.Championships.GetRacerInfo() on
                 new { r.FirstName, r.LastName } equals new { r2.FirstName, r2.LastName }
                 into yearResults
                 select new
                 {
                    r.FirstName,
                    r.LastName,
                    r.Wins,
                    r.Starts,
                    Results = yearResults
                 };

         foreach (var item in q)
         {
            Console.WriteLine("{0} {1}", item.FirstName, item.LastName);
            foreach (var item2 in item.Results)
            {
               Console.WriteLine("{0} {1}", item2.Year, item2.Position);
            }
         }
      }
   }
}
