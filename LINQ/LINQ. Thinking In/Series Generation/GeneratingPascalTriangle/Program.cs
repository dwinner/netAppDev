// Треугольник Паскаля

using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratingPascalTriangle
{
   internal static class Program
   {
      private static void Main()
      {
         var pascalValues = new List<Tuple<int, int, int>>
         {
            new Tuple<int, int, int>(1, 1, 1),
            new Tuple<int, int, int>(2, 1, 1),
            new Tuple<int, int, int>(2, 2, 1)
         };

         for (var i = 0; i < 10; i++)
         {
            var currentRow = pascalValues.Last().Item1 + 1;
            var currentCol = pascalValues.Last().Item2 + 1;
            for (var j = 1; j <= currentCol; j++)
            {
               if (j == 1 || j == currentCol)
               {
                  pascalValues.Add(new Tuple<int, int, int>(currentRow, j, 1));
               }
               else
               {
                  pascalValues.Add(new Tuple<int, int, int>(currentRow, j,
                     pascalValues.First(v => v.Item1 == currentRow - 1 && v.Item2 == j - 1).Item3 +
                     pascalValues.First(v => v.Item1 == currentRow - 1 && v.Item2 == j).Item3));
               }
            }
         }

         var dump =
            pascalValues.ToLookup(t => t.Item1, t => t.Item3.ToString())
               .Select(t => t.Aggregate((x, y) => string.Format("{0} {1}", x, y)))
               .Aggregate((u, v) => string.Format("{0}{1}{2}", u, Environment.NewLine, v));

         var outerDump =
            dump.Select(lineItem => dump.Aggregate(string.Empty, (item, pascalItem) => item + string.Format("{0} ", pascalItem)))
               .Aggregate(string.Empty,
                  (item, innerDump) => item + string.Format("{0}{1}", innerDump, Environment.NewLine));
         Console.WriteLine(outerDump);
      }
   }
}