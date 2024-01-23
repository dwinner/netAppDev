/**
 * Нахождение скалярного произведения векторов
 */

using System;
using System.Linq;

namespace FindingProductDot
{
   internal static class Program
   {
      private static void Main()
      {
         int[] firstVector = { 1, 2, 3 };
         int[] secondVector = { 3, 2, 1 };
         var vectorDotProduct = firstVector.Zip(secondVector, (a, b) => a * b);
         foreach (var coordinate in vectorDotProduct)
         {
            Console.WriteLine("Result: {0}", coordinate);
         }
      }
   }
}