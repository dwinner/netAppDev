/**
 * Четность числа и степень двойки
 */

using System;
// ReSharper disable UnusedMember.Local

namespace HowToCSharp.Ch05.PowerOfTwo
{
   internal static class PowerOfTwo
   {
      private static void Main()
      {
         for (long i = 0; i < long.MaxValue; i++)
            if (IsPowerOfTwo(i))
               Console.WriteLine("{0:N0} is a power of 2\t\t(Ctrl-C to stop)", i);

         Console.Write("Press any key to continue...");
         Console.ReadKey();
      }

      private static bool IsEven(long number) => (number & 0x1) == 0;

      private static bool IsPowerOfTwo(long number) => number != 0 && (number & -number) == number;
   }
}