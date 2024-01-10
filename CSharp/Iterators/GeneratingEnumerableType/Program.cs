/**
 * Генерация перечислимого типа
 */

using System;
using System.Collections.Generic;

namespace _12_GeneratingEnumerableType
{
   internal class Program
   {
      private static void Main()
      {
         var powers = Powers(0, 16);
         foreach (var power in powers)
         {
            Console.WriteLine(power);
         }

         Console.ReadKey();
      }

      public static IEnumerable<int> Powers(int from, int to)
      {
         for (var i = from; i <= to; i++)
         {
            yield return (int)Math.Pow(2, i);
         }
      }
   }
}