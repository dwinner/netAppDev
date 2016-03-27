/**
 * Трансформации
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Transform
{
   internal class Program
   {
      private static void Main()
      {
         var infiniteSeries = CreateInfiniteSeries();
         IEnumerable<double> transform = infiniteSeries.Transform(x => (double) x/3);
         var enumerator = transform.GetEnumerator();
         for (var i = 0; i < 25; i++)
         {
            enumerator.MoveNext();
            Console.WriteLine(enumerator.Current);
         }

         Console.ReadKey();
      }

      private static IEnumerable<int> CreateInfiniteSeries()
      {
         var n = 0;
         while (true)
         {
            yield return n++;
         }
      }
   }

   public static class MyExtensions
   {
      public static IEnumerable<TOut> Transform<TIn, TOut>(
         this IEnumerable<TIn> input,
         Func<TIn, TOut> transformOp)
      {
         return input.Select(transformOp);
      }
   }
}