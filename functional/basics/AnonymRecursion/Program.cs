/*
 * Анонимная рекурсия
 */

using System;

namespace _15_AnonymRecursion
{
   internal delegate TResult AnonRec<TArg, TResult>(AnonRec<TArg, TResult> f, TArg arg);

   internal static class Program
   {
      private static void Main()
      {
         AnonRec<int, int> factorial = (f, x) => x > 1 ? x * f(f, x - 1) : 1;
         var fiveFactor = factorial(factorial, 5);
         Console.WriteLine(fiveFactor);
      }
   }
}