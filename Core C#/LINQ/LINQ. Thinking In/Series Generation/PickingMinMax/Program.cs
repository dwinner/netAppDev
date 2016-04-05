// Получение максимальных/минимальных значений из двух совместимых массивов

using System;
using System.Collections.Generic;
using System.Linq;

namespace PickingMinMax
{
   internal static class Program
   {
      private static void Main()
      {
         var bidValues1 = new List<int> {1, 2, 3, 4, 5};
         var bidValues2 = new List<int> {2, 1, 4, 5, 6};
         var maximums = bidValues1.Zip(bidValues2, Math.Max);
         var minimums = bidValues1.Zip(bidValues2, Math.Min);

         Console.Write("Maximums: ");
         foreach (var maximum in maximums)
         {
            Console.Write("{0} ", maximum);
         }
         Console.WriteLine();

         Console.Write("Minimums: ");
         foreach (var minimum in minimums)
         {
            Console.Write("{0} ", minimum);
         }
         Console.WriteLine();
      }
   }
}