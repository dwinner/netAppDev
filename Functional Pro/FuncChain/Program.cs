/**
 * Создание функциональных цепочек
 */

using System;

namespace _11_FuncChain
{
   class Program
   {
      static void Main()
      {
         Func<int, double> func = Chain<int, int, double>(x => x * 3, x => x + Math.PI);
         Console.WriteLine(func(2));
         Console.ReadKey();
      }

      static Func<T, TS> Chain<T, TR, TS>(Func<T, TR> firstFunc, Func<TR, TS> secondFunc)
      {
         return x => secondFunc(firstFunc(x));
      }
   }
}
