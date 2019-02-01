using System;
using System.Diagnostics;

namespace AsyncDataReaders
{
   public static class MethodTimer
   {
      public static void TimeMethod(Action method, int iterations, string message)
      {
         Stopwatch stopwatch = Stopwatch.StartNew();
         for (int i = 0; i < iterations; i++) method();
         stopwatch.Stop();
         Console.WriteLine(message, iterations, stopwatch.ElapsedMilliseconds);
      }
   }
}