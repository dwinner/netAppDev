// Процентная статистика значений в наборе

using System;
using System.Collections.Generic;
using System.Linq;

namespace ScorePercentile
{
   internal static class Program
   {
      private static void Main()
      {
         int[] nums = { 20, 15, 31, 34, 35, 40, 50, 90, 99, 100 };
         var stat =
            nums.ToLookup(currentValue => currentValue, currentValue => nums.Where(numValue => numValue < currentValue))
               .Select(
                  grouping =>
                     new KeyValuePair<int, double>(grouping.Key, 100 * ((double)grouping.First().Count() / nums.Length)));
         foreach (var st in stat)
         {
            Console.WriteLine("Score: {0}\tOvertaken percent: {1}", st.Key, st.Value);
         }
      }
   }
}