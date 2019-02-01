/**
 * Цепочки операций
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_OperationChains
{
   internal class Program
   {
      private static void Main()
      {
         var divideByThree = new Func<int, double>(DivideByThree);
         var squareNumber = new Func<double, double>(Square);

         var result = CreateInfiniteList()
            .Transform(divideByThree)
            .Transform(squareNumber);

         var iter = result.GetEnumerator();
         for (var i = 0; i < 25; i++)
         {
            iter.MoveNext();
            Console.WriteLine(iter.Current);
         }

         Console.ReadKey();
      }

      private static IEnumerable<int> CreateInfiniteList()
      {
         var n = 0;
         while (true)
         {
            yield return n++;
         }
      }

      private static double DivideByThree(int n)
      {
         return (double)n / 3;
      }

      private static double Square(double r)
      {
         return r * r;
      }
   }

   public static class MyExtensions
   {
      public static IEnumerable<TOut> Transform<TIn, TOut>(this IEnumerable<TIn> input, Func<TIn, TOut> opFunc)
      {
         return input.Select(opFunc);
      }
   }
}