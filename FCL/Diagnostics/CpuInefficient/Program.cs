﻿using System.Diagnostics;

namespace CpuInefficient;

internal static class Program
{
   private static readonly Random Rnd = new();
   // This program is intentially written to perform poorly. You can run it while experimenting with the diagnostics techniques described in Chapter 13.

   private static void Main()
   {
      // The diagnostic tools need our process ID:
      Console.WriteLine($"Our process ID {Process.GetCurrentProcess().Id}");
      Console.WriteLine("Waiting 10 seconds to allow trace start.");
      Thread.Sleep(10000);
      Console.WriteLine(
         MaxSubarraySlow(LongRandomArray(7000))); // Input value 7000 runs for 1-2 minutes on modern hardware
   }

   // This poor implementation of the maximum subarray problem gives O(n^3) performance
   // For an O(n) solution, look up Kadane's algorithm.
   // This method calculates the largest sum you can get by adding the value at contiguous array indices.
   private static int MaxSubarraySlow(IReadOnlyList<int> array)
   {
      if (array.Count == 0)
      {
         throw new ArgumentException("Array must have elements.");
      }

      var maxTimer = Stopwatch.StartNew();
      var highestSum = int.MinValue;
      for (var i = 0; i < array.Count; i++) // Left bound of subarray
      for (var j = i + 1; j < array.Count; j++) // Right bound of subarray
      {
         var subarraySum = 0;
         for (var n = i; n <= j; n++)
         {
            subarraySum += array[n];
         }

         highestSum = Math.Max(highestSum, subarraySum);
         if (subarraySum == highestSum) // Max found (could equal prior max)
         {
            MyEventCounterSource.Log.Request(highestSum, maxTimer.ElapsedMilliseconds);
            maxTimer.Restart();
         }
      }

      return highestSum;
   }

   private static int[] LongRandomArray(int size)
   {
      return Enumerable.Repeat(0, size).Select(i => Rnd.Next()).ToArray();
   }
}