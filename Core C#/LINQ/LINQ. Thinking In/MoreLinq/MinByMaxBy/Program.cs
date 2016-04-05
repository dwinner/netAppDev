// Относительный минимум/максимум

using System;
using System.Linq;
using MoreLinq;

namespace MinByMaxBy
{
   internal static class Program
   {
      private static void Main()
      {
         int[] distances = { 23, 41, 11, 34, 45 };

         // Первый вариант
         int[] x = { distances[0] };
         foreach (var current in distances.Where(current => current - 10 < x[0] - 10))
         {
            x[0] = current;
         }
         Console.WriteLine(x[0]);

         // Второй вариант
         var minBy = distances.MinBy(a => a - 10);
         var min = distances.Min(a => a - 10);
         Console.WriteLine(minBy);
         Console.WriteLine(min);
      }
   }
}