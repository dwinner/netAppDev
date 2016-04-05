/**
 * Операции над выражениями
 */

using System;
using System.Linq.Expressions;

namespace _08_ExpressionOperations
{
   internal class Program
   {
      private static void Main()
      {
         Expression<Func<int, int>> expr = n => n + 1;

         // Теперь присвоить expr значение исходного
         // выражения, умноженное на 2
         expr = Expression.Lambda<Func<int, int>>(
            Expression.Multiply(expr.Body, Expression.Constant(2)),
            expr.Parameters
            );
         var func = expr.Compile();
         for (var i = 0; i < 10; i++)
         {
            Console.WriteLine(func(i));
         }

         Console.ReadLine();
      }
   }
}