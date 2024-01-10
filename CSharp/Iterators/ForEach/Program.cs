/*
 * Перебор элементов без привязки к реализации
 */

using System;
using System.Collections.Generic;

namespace ForEach
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         int[] array = { 1, 2, 3, 4, 5 };
         foreach (var n in array)
         {
            Console.Write("{0} ", n);
         }

         Console.WriteLine();
         var times = new List<DateTime>(new[] { DateTime.Now, DateTime.UtcNow });
         foreach (var time in times)
         {
            Console.WriteLine(time);
         }

         var numbers = new Dictionary<int, string>();
         numbers[1] = "One";
         numbers[2] = "Two";
         numbers[3] = "Three";
         foreach (var pair in numbers)
         {
            Console.WriteLine("{0}", pair);
         }

         Console.ReadKey();
      }
   }
}