// Последовательность средних значений соседних элементов

using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace WindowedOperator
{
   internal static class Program
   {
      private static void Main()
      {
         int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
         var windowVals = new List<List<int>>();
         for (var i = 0; i < values.Length - 1; i++)
         {
            var inner = new List<int>();
            for (var j = i; j < i + 2; j++)
            {
               inner.Add(values[j]);
            }

            windowVals.Add(inner);
         }

         var movingAvgs =
            from ints in windowVals
            let avg = ints.Aggregate<int, double>(0, (accumulator, currentVal) => accumulator + currentVal)
            select avg / ints.Count;
         
         movingAvgs.ForEach(Console.WriteLine);         
      }
   }
}