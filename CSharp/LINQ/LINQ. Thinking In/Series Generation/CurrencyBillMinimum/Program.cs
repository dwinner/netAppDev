using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyBillMinimum
{
   internal static class Program
   {
      private static void Main()
      {
         int[] curvals = { 500, 100, 50, 20, 10, 5, 2, 1, 1000 };
         var amount = 2548;
         var map = new Dictionary<int, int>();
         curvals.OrderByDescending(c => c).ToList().ForEach(c =>
         {
            map.Add(c, amount / c);
            amount %= c;
         });
         var values = map.Where(pair => map.Values.Count != 0);
         foreach (var val in values)
         {
            Console.WriteLine("Key: {0}. Value: {1}", val.Key, val.Value);
         }
      }
   }
}