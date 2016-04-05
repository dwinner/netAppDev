// Ранжирование результатов по занимаемому месту

using System;
using System.Linq;

namespace ScoreRanking
{
   internal static class Program
   {
      private static void Main()
      {
         int[] marks = { 20, 15, 31, 34, 35, 50, 40, 90, 99, 100 };
         var ranking =
            marks.ToLookup(k => k, k => marks.Where(mark => mark >= k))
               .Select(k => new { Marks = k.Key, Rank = 10 * ((double)k.First().Count() / (double)marks.Length) });
         foreach (var rank in ranking)
         {
            Console.WriteLine(rank);
         }
      }
   }
}