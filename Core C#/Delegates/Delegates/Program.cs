/**
 * Динамический вызов метода
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegates
{
   class Program
   {
      delegate T MathOp<T>(T a, T b);

      static void Main()
      {
         // Да, это массив методов,
         // и все они соответствуют делегату MathOp<T>
         var opsList = new List<MathOp<double>> { Add, Divide, Add, Multiply };

         double result = opsList.Aggregate(0.0, (current, op) => op(current, 3));
         Console.WriteLine("result = {0}", result);

         Console.ReadKey();
      }

      static double Divide(double a, double b)
      {
         return a / b;
      }

      static double Multiply(double a, double b)
      {
         return a * b;
      }

      static double Add(double a, double b)
      {
         return a + b;
      }
   }
}
