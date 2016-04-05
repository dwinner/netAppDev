/**
 * Лямбда-выражение с парметром
 */

using System;

namespace _02_ParamlessLambda
{
   class Program
   {
      static void Main()
      {
         int counter = 0;
         WriteStream(() => counter++);
         Console.WriteLine("Финальное значение счетчика: {0}", counter);   // Note: 10 из-за замыкания counter
         Console.ReadLine();
      }

      static void WriteStream(Func<int> generator)
      {
         for (int i = 0; i < 10; i++)
         {
            Console.Write("{0}, ", generator());
         }
         Console.WriteLine();
      }
   }
}
