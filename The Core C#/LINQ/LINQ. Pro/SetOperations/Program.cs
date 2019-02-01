/**
 * Операции с множествами
 */

using System;
using System.Collections.Generic;
using System.Linq;
using DataLib;

namespace SetOperations
{
   static class Program
   {
      static void Main()
      {
         IntersectSample();
         Console.WriteLine();

         ZipOperation();
         Console.WriteLine();

         UnionSample();
         Console.WriteLine();

         DistinctSample();
         Console.WriteLine();

         ExceptSample();
         Console.WriteLine();
      }

      #region Пересечение множеств

      private static void IntersectSample()
      {
         Func<string, IEnumerable<Racer>> racersByCar = car => from racer in Formula1.Champions
                                                               from c in racer.Cars
                                                               where c == car
                                                               orderby racer.LastName
                                                               select racer;
         Console.WriteLine("World champion with Ferrari and McLaren");
         foreach (var racer in racersByCar("Ferrari").Intersect(racersByCar("McLaren")))
         {
            Console.WriteLine(racer);
         }
      }

      #endregion

      #region Поэлементное объединение множеств по заданной структуре

      private static void ZipOperation()
      {
         var racerNames = from racer in Formula1.Champions
                          where racer.Country == "Italy"
                          orderby racer.Wins descending
                          select new
                          {
                             Name = string.Format("{0} {1}", racer.FirstName, racer.LastName)
                          };

         var racerNamesAndStarts = from racer in Formula1.Champions
                                   where racer.Country == "Italy"
                                   orderby racer.Wins descending
                                   select new
                                   {
                                      racer.LastName,
                                      racer.Starts
                                   };

         var racers = racerNames.Zip(racerNamesAndStarts,
            (first, second) => string.Format("{0}, starts: {1}", first.Name, second.Starts));

         foreach (var racer in racers)
         {
            Console.WriteLine(racer);
         }
      }

      #endregion

      #region Объединение множеств

      private static void UnionSample()
      {
         IEnumerable<string> heavy = new List<string>()
         {
            "Running wild",
            "Manowar",
            "Primal Fear",
            "Sinner",
         };

         IEnumerable<string> hard = new List<string>()
         {
            "Sinner",
            "Primal Fear",
            "Shakra",
            "Gotthard",
            "AC/DC"
         };

         foreach (var g in heavy.Union(hard))
         {
            Console.WriteLine(g);
         }
      }

      #endregion

      #region Удаление дубликатов

      private static void DistinctSample()
      {
         IEnumerable<string> heavy = new List<string>()
         {
            "Running wild",
            "Manowar",
            "Primal Fear",
            "Sinner",
         };

         IEnumerable<string> hard = new List<string>()
         {
            "Sinner",
            "Primal Fear",
            "Shakra",
            "Gotthard",
            "AC/DC"
         };

         var hardAndHeavy = new List<string>(heavy);
         hardAndHeavy.AddRange(hard);

         var disticting = hardAndHeavy.Distinct();
         foreach (var s in disticting)
         {
            Console.WriteLine(s);
         }
      }

      #endregion

      #region Вычитание множеств

      private static void ExceptSample()
      {
         var racers = Formula1.Championships.SelectMany(championship => new List<RacerInfo>
         {
            new RacerInfo
            {
               Year = championship.Year,
               Position = 1,
               FirstName = championship.First.FirstName(),
               LastName = championship.Second.LastName()
            },
            new RacerInfo
            {
               Year = championship.Year,
               Position = 2,
               FirstName = championship.First.FirstName(),
               LastName = championship.Second.LastName()
            },
            new RacerInfo
            {
               Year = championship.Year,
               Position = 3,
               FirstName = championship.First.FirstName(),
               LastName = championship.Second.LastName()
            }
         });

         var nonChampions = racers.Select(racerInfo => new
         {
            racerInfo.FirstName,
            racerInfo.LastName
         }).Except(Formula1.Champions.Select(racer => new
         {
            racer.FirstName,
            racer.LastName
         }));

         foreach (var nonChampion in nonChampions)
         {
            Console.WriteLine("{0} {1}", nonChampion.FirstName, nonChampion.LastName);
         }
      }

      #endregion

   }
}
