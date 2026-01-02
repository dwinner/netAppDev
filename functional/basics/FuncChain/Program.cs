/*
 * Создание функциональных цепочек
 */

using System;

namespace _11_FuncChain
{
   internal static class Program
   {
      private static void Main()
      {
         var func = Chain<int, int, double>(x => x * 3, x => x + Math.PI);
         Console.WriteLine(func(2));
         Console.ReadKey();
      }

      private static Func<T, TS> Chain<T, TR, TS>(Func<T, TR> func1, Func<TR, TS> func2)
      {
         return x => func2(func1(x));
      }
   }
}