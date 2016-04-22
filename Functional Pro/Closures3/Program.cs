/**
 * Замыкания в C# 2.0
 */

using System;

namespace _05_ClosureViaSecondCSharp
{
   class Program
   {
      static void Main()
      {
         int counter = 0;
         WriteStream(delegate { return counter++; });
         Console.WriteLine("Финальное значение счетчика: {0}", counter);

         Console.ReadLine();
      }

      static void WriteStream(Func<int> counter)
      {
         for (int i = 0; i < 10; i++)
         {
            Console.Write("{0}, ", counter());
         }
         Console.WriteLine();
      }
   }
}
