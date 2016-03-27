/**
 * Множества
 */

using System;
using System.Collections.Generic;

namespace SetSample
{
   internal static class Program
   {
      static void Main()
      {
         var companyTeams = new HashSet<string> { "Ferrari", "McLaren", "Mercedes" };
         var traditionalTeams = new HashSet<string> { "Ferrari", "McLaren" };
         var privateTeams = new HashSet<string> { "Red Bull", "Lotus", "Toro Rosso", "Force India", "Sauber" };

         var groups = new HashSet<string>
         {
            "Manowar",
            "Running Wild",
            "Grave Digger",
            "Helloween",
            "Metalium",
            "Axxis",
            "Gotthard"
         };
         var hardRockGroups = new HashSet<string> { "Gotthard", "Axxis", "Udo", "Scorpions" };
         var powerGroups = new HashSet<string> { "Grave Digger", "Helloween", "Metalium" };
         var heavyGroups = new HashSet<string> { "Grave Digger", "Manowar", "Running Wild" };

         if (privateTeams.Add("Williams"))
            Console.WriteLine("Williams added");
         if (!companyTeams.Add("McLaren"))
            Console.WriteLine("McLaren was already in this set");

         #region Является ли множество подмножеством другого
         
         if (traditionalTeams.IsSubsetOf(companyTeams))
         {
            Console.WriteLine("traditionalTeams is subset of companyTeams");
         }

         #endregion

         #region Является ли множество надмножеством другого

         if (companyTeams.IsSupersetOf(traditionalTeams))
         {
            Console.WriteLine("companyTeams is a superset of traditionalTeams");
         }

         #endregion

         #region Пересекаются ли множества

         traditionalTeams.Add("Williams");
         if (privateTeams.Overlaps(traditionalTeams))
         {
            Console.WriteLine("At least one team is the same with the traditional " +
                              "and private teams");
         }

         #endregion

         #region Объединение множеств

         var allTeams = new SortedSet<string>(companyTeams);
         allTeams.UnionWith(privateTeams);
         allTeams.UnionWith(traditionalTeams);

         Console.WriteLine();
         Console.WriteLine("all teams");
         foreach (var team in allTeams)
         {
            Console.WriteLine(team);
         }

         #endregion

         #region Вычитание множеств

         allTeams.ExceptWith(privateTeams);
         Console.WriteLine();
         Console.WriteLine("no private team left");
         foreach (var team in allTeams)
         {
            Console.WriteLine(team);
         }

         #endregion

         #region Пересечение множеств

         powerGroups.IntersectWith(heavyGroups);   // Только "Grave Digger" является Power и Heavy
         foreach (string powerAndHeavy in powerGroups)
         {
            Console.WriteLine(powerAndHeavy);
         }

         #endregion

         #region Симметричное исключение

         // "Gotthard" и "Axxis" не попадут в результирующее множество, т.к. присутствовали в обоих одновременно
         groups.SymmetricExceptWith(hardRockGroups);
         Console.WriteLine("Symmetric except:");
         foreach (string s in groups)
         {
            Console.WriteLine(s);
         }

         #endregion
      }
   }
}
