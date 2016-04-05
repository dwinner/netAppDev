/**
 * Четность числа и степень двойки
 */
using System;

namespace HowToCSharp.Ch05.PowerOfTwo
{
   class PowerOfTwo
   {
      static void Main()
      {
         for (Int64 i = 0; i < Int64.MaxValue; i++)
         {
            if (IsPowerOfTwo(i))
            {
               Console.WriteLine("{0:N0} is a power of 2\t\t(Ctrl-C to stop)", i);
            }
         }

         Console.Write("Press any key to continue...");
         Console.ReadKey();
      }

      private static bool IsEven(Int64 number)
      {
         return ((number & 0x1) == 0);
      }

      private static bool IsPowerOfTwo(Int64 number)
      {
         return (number != 0) && ((number & -number) == number);
      }
   }
}
