/**
 * Явные деревья выражений
 */

using System;
using System.Linq.Expressions;

namespace _07_ExplicitExpressionTrees
{
   class Program
   {
      static void Main()
      {
         var n = Expression.Parameter(typeof(int), "n");
         var expr = Expression.Lambda<Func<int, int>>(Expression.Add(n, Expression.Constant(1)), n);
         Func<int, int> func = expr.Compile();
         for (int i = 0; i < 10; i++)
         {
            Console.WriteLine(func(i));
         }

         Console.ReadLine();
      }
   }
}
