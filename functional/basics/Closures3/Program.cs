/*
 * Замыкания в C# 2.0
 */

using System;

namespace _05_ClosureViaSecondCSharp
{
   internal static class Program
   {
      private static void Main()
      {
         var counter = 0;
         WriteStream(delegate { return counter++; });
         Console.WriteLine("Финальное значение счетчика: {0}", counter);

         Console.ReadLine();
      }

      private static void WriteStream(Func<int> counter)
      {
         for (var i = 0; i < 10; i++)
         {
            Console.Write("{0}, ", counter());
         }

         Console.WriteLine();
      }
   }
}