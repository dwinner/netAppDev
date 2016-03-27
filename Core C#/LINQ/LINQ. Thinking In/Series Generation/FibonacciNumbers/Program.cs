// Числа Фибоначчи

using System;
using System.Collections.Generic;
using System.Linq;

namespace FibonacciNumbers
{
   internal static class Program
   {
      private static void Main()
      {
         var fibonacciNumbers = new List<ulong>();
         Enumerable.Range(0, 200)
            .ToList()
            .ForEach(k => fibonacciNumbers.Add(k <= 1 ? 1 : fibonacciNumbers[k - 2] + fibonacciNumbers[k - 1]));
         foreach (var fib in fibonacciNumbers.Take(10))
         {
            Console.Write("{0} - ", fib);
         }
         Console.WriteLine();
      }
   }
}