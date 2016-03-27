/**
 * Генерация перечислимого типа
 */

using System;
using System.Collections.Generic;

namespace _12_GeneratingEnumerableType
{
   class Program
   {
      static void Main()
      {
         IEnumerable<int> powers = Powers(0, 16);
         foreach (int power in powers)
         {
            Console.WriteLine(power);
         }

         Console.ReadKey();
      }

      public static IEnumerable<int> Powers(int from, int to)
      {
         for (int i = from; i <= to; i++)
         {
            yield return (int)Math.Pow(2, i);
         }
      }
   }
}
