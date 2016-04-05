/**
 * Секундомер для простого способа профилирования
 */

using System;
using System.Diagnostics;

namespace AutoStopwatchSample
{
   class Program
   {
      private const int Limit = 1000000;

      static void Main()
      {
         StopwatchSample();
         Console.WriteLine();

         using (new AutoStopwatch())
         {
            decimal total = 0;
            for (int i = 0; i < Limit; i++)
            {
               total += (decimal)Math.Sqrt(i);
            }
            Console.WriteLine(total);
         }
      }

      #region Методы для проведения измерений

      private static decimal MeasureTime1()
      {
         decimal total = 0;
         const int limit = 1000000;
         for (int i = 0; i < limit; i++)
         {
            total = total + (decimal)Math.Sqrt(i);
         }

         return total;
      }

      private static decimal MeasureTime2()
      {
         decimal total = 0;
         const int limit = 1000000;
         for (int i = 0; i < limit; i++)
         {
            total += (decimal)Math.Sqrt(i);
         }

         return total;
      }

      #endregion

      internal static void StopwatchSample()
      {
         var stopwatch = new Stopwatch();

         stopwatch.Start();
         decimal sqrt1 = MeasureTime1();
         stopwatch.Stop();
         Console.WriteLine("Sum os sqrts: {0}", sqrt1);
         Console.WriteLine("Elapsed milliseconds: {0}", stopwatch.ElapsedMilliseconds);
         Console.WriteLine("Elapsed time: {0}", stopwatch.Elapsed);

         stopwatch.Restart();
         decimal sqrt2 = MeasureTime2();
         stopwatch.Stop();
         Console.WriteLine("Sum os sqrts: {0}", sqrt2);
         Console.WriteLine("Elapsed milliseconds: {0}", stopwatch.ElapsedMilliseconds);
         Console.WriteLine("Elapsed time: {0}", stopwatch.Elapsed);
      }

      internal static void AutostopWatcher()
      {

      }
   }
}
