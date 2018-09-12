/**
 * Pattern matching
 */

using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace PatternMatchingSample
{
   internal static class Program
   {
      private static void Main()
      {
         (int, double) FilterNumbers()
         {
            object[] numbers = {"str", 0, 0, 1, 6, null, 0.0, 7.9, 5, 5};
            var r =
               (ints: numbers.Where(n => n is int i).Cast<int>().Sum(),
               doubles: numbers.Where(d => d is double dd).Cast<double>().Sum());

            return r;
         }

         var t = FilterNumbers();
         WriteLine($"{t.Item1}, {t.Item2}");

         // Flexible switch
         IEnumerable<object> list = new List<object> {1, 2, 3, 4, null, "str", Guid.Empty};
         var p = (s: 0, c: 0);
         foreach (var v in list)
            switch (v)
            {
               case int i:
                  p.s += i;
                  p.c++;
                  break;

               case IEnumerable<object> l when l.Any():
                  p.s += l.OfType<int>().Sum();
                  break;

               case null:
                  break;
            }
      }
   }
}