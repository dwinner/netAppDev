using System;
using System.Collections.Generic;
using SearchAlgs.Lib;
using static System.Console;

namespace BinarySearch.Test
{
   internal static class Program
   {
      private static void Main()
      {
         var generator = new Random();
         var data = new int[15]; // create space for array

         // fill array with random ints in range 10-99
         for (var i = 0; i < data.Length; ++i) data[i] = generator.Next(10, 100);

         Array.Sort(data); // elements must be sorted in ascending order
         DisplayElements(data, 0, data.Length - 1); // display array

         // input first int from user
         Write($"{Environment.NewLine}Please enter an integer value (-1 to quit): ");
         var searchInt = int.Parse(ReadLine());

         // repeatedly input an integer; -1 terminates the app
         while (searchInt != -1)
         {
            // perform binary search
            var position = Searching.BinarySearch(data, searchInt);

            if (position != -1) // integer was found
               WriteLine($"The integer {searchInt} was found in " +
                         $"position {position}.\n");
            else // integer was not found
               WriteLine(
                  $"The integer {searchInt} was not found.\n");

            // input next int from user
            Write("Please enter an integer value (-1 to quit): ");
            searchInt = int.Parse(ReadLine());
         }
      }

      private static void DisplayElements(IReadOnlyList<int> values, int low, int high)
      {
         // output three spaces for each element up to low for alignment
         Write(string.Empty.PadLeft(low * 3));

         // output elements left in array
         for (var i = low; i <= high; ++i) Write($"{values[i]} ");

         WriteLine();
      }
   }
}