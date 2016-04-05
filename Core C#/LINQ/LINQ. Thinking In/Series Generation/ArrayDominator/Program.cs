// Доминантное значение массива

using System;
using System.Linq;

namespace ArrayDominator
{
   internal static class Program
   {
      private static void Main()
      {
         int[] array = { 3, 4, 3, 2, 3, -1, 3, 3 };
         var dominator = array.ToLookup(k => k).FirstOrDefault(kGrouping => kGrouping.Count() > array.Length / 2);
         if (dominator != null)
         {
            Console.WriteLine("Array dominator value: {0}", dominator.Key);
         }
      }
   }
}