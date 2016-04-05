// Префиксное суммирование последовательности

using System;
using MoreLinq;

namespace Scan
{
   internal static class Program
   {
      private static void Main()
      {
         // Первый способ
         int[] numbers = { 1, 2, 3, 4 };
         var sums = new int[numbers.Length];
         for (var i = 0; i < numbers.Length; i++)
         {
            for (var j = 0; j <= i; j++)
            {
               sums[i] += numbers[j];
            }
         }
         sums.ForEach(i => Console.Write("{0} ", i));

         Console.WriteLine();

         // Второй способ
         int[] numbers2 = { 1, 2, 3, 4 };
         var scanned = numbers2.Scan((a, b) => a + b);
         scanned.ForEach(i => Console.Write("{0} ", i));

         Console.WriteLine();
      }
   }
}