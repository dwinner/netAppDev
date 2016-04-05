// Удаление дубликатов последовательности на месте

using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace PairWiseSample
{
   internal static class Program
   {
      private static void Main()
      {
         var uniques = "LLIIIINNQQ".ToCharArray().RemoveConsecutiveDuplicates();
         uniques.ForEach(c => Console.Write("{0} ", c));
         Console.WriteLine();
      }
   }

   public static class MyLinqEx
   {
      public static IEnumerable<T> RemoveConsecutiveDuplicates<T>(this IEnumerable<T> input)
         where T : IComparable<T>
      {
         var notDefInpt = input as T[] ?? input.ToArray();
         var conditions = notDefInpt.Pairwise((a, b) => a.Equals(b));
         var dontPickIndices = conditions.Index().Where(c => c.Value).Select(k => k.Key);
         return
            Enumerable.Range(0, notDefInpt.Length).Where(e => !dontPickIndices.Contains(e)).Select(notDefInpt.ElementAt);
      }
   }
}