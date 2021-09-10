/**
 * Подсчет битов в числе
 */

using System;

namespace HowToCSharp.Ch05.CountBits
{
   internal static class CountBitsProgram
   {
      private static void Main()
      {
         for (var i = 0; i < 25; i++)
            Console.WriteLine("{0} bits set in {1} ({2})", CountBits(i), i, Convert.ToString(i, 2));
         Console.ReadKey();
      }

      private static short CountBits(int number)
      {
         var accum = number;
         short count = 0;
         while (accum > 0)
         {
            accum &= accum - 1;
            count++;
         }
         return count;
      }
   }
}