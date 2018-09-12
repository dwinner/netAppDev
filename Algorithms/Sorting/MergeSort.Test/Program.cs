using System;
using System.Diagnostics;
using StandartSortingAlgs.Lib;

namespace MergeSort.Test
{
   internal static class Program
   {
      private static void Main()
      {
         var generator = new Random();
         var data = new int[10]; // create space for array

         // fill array with random ints in range 10-99
         for (var i = 0; i < data.Length; ++i) data[i] = generator.Next(10, 100);

         Console.WriteLine("Unsorted array:");
         Console.WriteLine(string.Join(" ", data) + Environment.NewLine); // display array

         var stopwatch = Stopwatch.StartNew();
         SortingAlgs.MergeSort(data); // sort array
         var elapsed = stopwatch.Elapsed;
         Console.WriteLine($"Elapsed: {elapsed}");

         for (var i = 0; i < data.Length; ++i) data[i] = generator.Next(10, 100);

         stopwatch.Restart();
         SortingAlgs.MergeSort(data); // sort array
         elapsed = stopwatch.Elapsed;
         Console.WriteLine($"Elapsed: {elapsed}");

         Console.WriteLine("Sorted array:");
         Console.WriteLine(string.Join(" ", data) + Environment.NewLine); // display array
      }
   }
}