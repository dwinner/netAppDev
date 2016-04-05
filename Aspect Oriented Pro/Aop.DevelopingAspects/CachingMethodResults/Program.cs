// Прозрачный способ кэширования результатов работы метода

using System;
using System.Diagnostics;

namespace CachingMethodResults
{
   internal static class Program
   {
      private const int FibCount = 40;

      private static void Main()
      {
         FibTest();
         CachedFibTest();
      }

      private static void FibTest()
      {
         var stimer = new Stopwatch();
         stimer.Start();
         Fib(FibCount);
         Console.WriteLine(stimer.ElapsedMilliseconds);
      }

      private static void CachedFibTest()
      {
         var stimer = new Stopwatch();
         stimer.Start();
         FastFib(FibCount);
         Console.WriteLine(stimer.ElapsedMilliseconds);
      }

      private static int Fib(int x) => x > 1 ? Fib(x - 1) + Fib(x - 2) : x;

      [Cache]
      private static int FastFib(int x) => x > 1 ? FastFib(x - 1) + FastFib(x - 2) : x;
   }
}