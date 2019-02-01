/**
 * Операции соединения
 */

using System;
using System.Collections.Generic;
using System.Linq;
using DataLib;

namespace Joining
{
   static class Program
   {
      static void Main()
      {
         InnerJoin();
         Console.WriteLine();

         LeftOuterJoin();
         Console.WriteLine();

         GroupJoin();
         Console.WriteLine();
      }

      #region Внутреннее соединение

      private static void InnerJoin()
      {
         var racers = from racer in Formula1.Champions
                      from year in racer.Years
                      select new
                      {
                         Year = year,
                         Name = string.Format("{0} {1}", racer.FirstName, racer.LastName)
                      };

         var teams = from team in Formula1.ConstructorChampions
                     from year in team.Years
                     select new
                     {
                        Year = year,
                        team.Name
                     };

         var racersAndTeams = (from racer in racers
                               join team in teams on racer.Year equals team.Year
                               orderby team.Year
                               select new
                               {
                                  racer.Year,
                                  Champion = racer.Name,
                                  Constructor = team.Name
                               }).Take(10);

         Console.WriteLine("Year World Champion\t   Constructor Title");
         foreach (var racerAndTeam in racersAndTeams)
         {
            Console.WriteLine("{0}: {1,-20} {2}",
               racerAndTeam.Year, racerAndTeam.Champion, racerAndTeam.Constructor);
         }
      }

      #endregion

      #region Левое внешнее соединение

      private static void LeftOuterJoin()
      {
         var racers = from racer in Formula1.Champions
                      from year in racer.Years
                      select new
                      {
                         Year = year,
                         Name = racer.FirstName + " " + racer.LastName
                      };

         var teams = from team in Formula1.ConstructorChampions
                     from year in team.Years
                     select new
                     {
                        Year = year,
                        team.Name
                     };

         var racersAndTeams = (from racer in racers
                               join team in teams on racer.Year equals team.Year
                                  into racersTeams
                               from t in racersTeams.DefaultIfEmpty()
                               orderby racer.Year
                               select new
                               {
                                  racer.Year,
                                  Champion = racer.Name,
                                  Constructor = t == null ? "No constructor championship" : t.Name
                               }).Take(10);

         Console.WriteLine("Year  Champion\t\t   Constructor Title");
         foreach (var item in racersAndTeams)
         {
            Console.WriteLine("{0}: {1,-20} {2}",
               item.Year, item.Champion, item.Constructor);
         }
      }

      #endregion

      #region Соединение с группировкой

      private static void GroupJoin()
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

         var q = from champion in Formula1.Champions
                 join racer in racers on
                 new
                 {
                    champion.FirstName,
                    champion.LastName
                 }
                 equals
                 new
                 {
                    racer.FirstName,
                    racer.LastName
                 }
                 into yearResults
                 select new
                 {
                    champion.FirstName,
                    champion.LastName,
                    champion.Wins,
                    champion.Starts,
                    Results = yearResults
                 };

         foreach (var r in q)
         {
            Console.WriteLine("{0} {1}", r.FirstName, r.LastName);
            foreach (var result in r.Results)
            {
               Console.WriteLine("{0} {1}", result.Year, result.Position);
            }
         }
      }

      #endregion
   }
}
