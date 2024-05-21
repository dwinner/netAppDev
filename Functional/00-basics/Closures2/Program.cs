/*
 * Замыкания в C# 1.0
 */

using System;

namespace _04_ClosureViaFisrtCSharp
{
   internal static class Program
   {
      private static unsafe void Main()
      {
         var counter = 0;
         var closure = new MyClosure(&counter);
         WriteStream(closure.GetDelegate());
         Console.WriteLine("Финальное значение счетчика: {0}", counter);
         Console.ReadLine();
      }

      private static void WriteStream(MyClosure.IncDelegate incrementor)
      {
         for (var i = 0; i < 10; i++)
         {
            Console.Write("{0}, ", incrementor());
         }
      }
   }
}