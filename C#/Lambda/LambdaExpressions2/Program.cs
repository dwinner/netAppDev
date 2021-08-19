/**
 * Лямбда-выражения
 */

using System;

namespace LambdaExpressions
{
   static class Program
   {
      static void Main()
      {
         const int someVal = 5;
         const int val = someVal;
         Func<int, int> f = x => x + val;
         /*
                  someVal = 7;
         */
         Console.WriteLine(f(3));

         SimpleDemos();

         Console.ReadLine();
      }

      static void SimpleDemos()
      {
         Func<string, string> oneParam = s => string.Format("change uppercase {0}", s.ToUpper());
         Console.WriteLine(oneParam("test"));

         Func<double, double, double> twoParams = (x, y) => x * y;
         Console.WriteLine(twoParams(3, 2));

         Func<double, double, double> twoParamsWithTypes = (x, y) => x * y;
         Console.WriteLine(twoParamsWithTypes(4, 2));

         Func<double, double> operations = x => x * 2;
         operations += x => x * x;

         ProcessAndDisplayNumber(operations, 2.0);
         ProcessAndDisplayNumber(operations, 7.94);
         ProcessAndDisplayNumber(operations, 1.414);
         Console.WriteLine();
      }

      static void ProcessAndDisplayNumber(Func<double, double> action, double value)
      {
         double result = action(value);
         Console.WriteLine("Value is {0}, result of operation is {1}", value, result);

      }
   }
}
