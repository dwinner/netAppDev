using System;
using System.Collections.Generic;
using System.Linq;

namespace MovingAverage
{
   internal static class Program
   {
      private static void Main()
      {
         var numbers = new List<double> { 1, 2, 3, 4 };
         var movingAvgs = new List<double>();
         const int windowSize = 2;
         Enumerable.Range(0, numbers.Count - windowSize + 1)
            .ToList()
            .ForEach(k => movingAvgs.Add(numbers.Skip(k).Take(windowSize).Average()));
         movingAvgs.ForEach(Console.WriteLine);
      }
   }
}