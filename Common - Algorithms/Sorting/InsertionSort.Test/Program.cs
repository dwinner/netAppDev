using System;
using StandartSortingAlgs.Lib;
using static System.Console;

namespace InsertionSort.Test
{
   internal static class Program
   {
      private static void Main()
      {
         var generator = new Random();
         var data = new int[10]; // create space for array

         // fill array with random ints in range 10-99
         for (var i = 0; i < data.Length; ++i) data[i] = generator.Next(10, 100);

         WriteLine("Unsorted array:");
         WriteLine(string.Join(" ", data) + Environment.NewLine); // display array

         SortingAlgs.InsertionSort(data); // sort array

         WriteLine("Sorted array:");
         WriteLine(string.Join(" ", data) + Environment.NewLine); // display array
      }
   }
}