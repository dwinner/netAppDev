using System;
using System.Linq;

namespace WeightedSum
{
   internal static class Program
   {
      private static void Main()
      {
         int[] vectors = { 1, 2, 3 };
         int[] weights = { 3, 2, 1 };
         var multiplication = vectors.Zip(weights, (value, weight) => value * weight).Sum();
         Console.WriteLine(multiplication);
      }
   }
}