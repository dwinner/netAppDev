/**
 * Лямбда-выражение двумя параметрами
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_TwoParamsLambda
{
   class Program
   {
      static void Main()
      {
         var teamMembers = new List<string>
         {
            "Lou Loomis",
            "Smoke Porterhouse",
            "Danny Noonan",
            "Ty Webb"
         };
         FindByFirstName(teamMembers, "Danny", (x, y) => x.Contains(y));
         Console.ReadKey();
      }

      static void FindByFirstName(IEnumerable<string> members, string firstName, Func<string, string, bool> predicate)
      {
         foreach (var member in members.Where(member => predicate(member, firstName)))
         {
            Console.WriteLine(member);
         }
      }
   }
}
