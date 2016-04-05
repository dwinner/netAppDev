/**
 * Конструкция from и переменные диапазона
 */

using System;
using System.Linq;

namespace _03_FromAndDimensionVars
{
   class Program
   {
      static void Main()
      {
         var query = from x in Enumerable.Range(0, 10)
                     from y in Enumerable.Range(0, 10)
                     select new { X = x, Y = y, Product = x * y };
         foreach (var item in query)
         {
            Console.WriteLine("{0} * {1} = {2}", item.X, item.Y, item.Product);
         }

         Console.ReadLine();
      }
   }
}
