/**
 * Динамические методы
 */

using System;
using System.Collections.Generic;
using System.Dynamic;

namespace DynamicMethods
{
   static class Program
   {
      static void Main()
      {
         dynamic dynamicExpressions = new ExpandoObject();

         // Добавить коэффициенты
         dynamicExpressions.Coefficients = new List<dynamic>();
         dynamicExpressions.Coefficients.Add(2);
         dynamicExpressions.Coefficients.Add(3);
         dynamicExpressions.Coefficients.Add(5);

         // Создать динамический метод
         dynamicExpressions.Compute = new Func<double, double>(x =>
         {
            double result = 0;
            for (int i = 0; i < dynamicExpressions.Coefficients.Count; i++)
            {
               result += (double)dynamicExpressions.Coefficients[i] * Math.Pow(x, i);
            }
            return result;
         });

         // Вычислить значение
         Console.WriteLine(dynamicExpressions.Compute(2));

         Console.ReadKey();
      }
   }
}
