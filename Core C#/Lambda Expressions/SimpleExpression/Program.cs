/**
 * Выражение делегатов в терминах лямбда
 */

using System;
using System.Linq.Expressions;

namespace _06_SimpleExpression
{
   static class Program
   {
      static void Main()
      {
         Expression<Func<int, int>> expr = n => n + 1;
         Func<int, int> func = expr.Compile();
         for (int i = 0; i < 10; i++)
         {
            Console.WriteLine(func(i));
         }
         Console.ReadKey();
      }
   }
}
