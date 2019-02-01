/**
 * Замыкания в C# 1.0
 */

using System;

namespace _04_ClosureViaFisrtCSharp
{
   class Program
   {
      unsafe static void Main()
      {
         int counter = 0;
         var closure = new MyClosure(&counter);
         WriteStream(closure.GetDelegate());
         Console.WriteLine("Финальное значение счетчика: {0}", counter);
         Console.ReadLine();
      }

      static void WriteStream(MyClosure.IncDelegate incrementor)
      {
         for (int i = 0; i < 10; i++)
         {
            Console.Write("{0}, ", incrementor());
         }
      }
   }

   unsafe public class MyClosure
   {
      private readonly int* _counter;

      public MyClosure(int* counter)
      {
         _counter = counter;
      }

      public delegate int IncDelegate();

      public IncDelegate GetDelegate()
      {
         return IncrementFunction;
      }

      private int IncrementFunction()
      {
         return (*_counter)++;
      }
   }
}
