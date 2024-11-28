using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ExceptionCost
{
   internal static class Program
   {
      private const int NumIterations = 1_000;

      private static void Main()
      {
         // first get them to JIT themselves
         EmptyMethod();
         try
         {
            ExceptionMethod(1);
         }
         catch (InvalidOperationException)
         {
         }

         var watch = new Stopwatch();

         watch.Restart();
         for (var i = 0; i < NumIterations; i++)
         {
            EmptyMethod();
         }

         watch.Stop();
         var baselineTime = watch.ElapsedTicks;
         Console.WriteLine("Empty Method: 1x");

         for (var depth = 1; depth <= 10; depth++)
         {
            watch.Restart();
            for (var i = 0; i < NumIterations; i++)
            {
               try
               {
                  ExceptionMethod(depth);
               }
               catch (InvalidOperationException)
               {
               }
            }

            watch.Stop();
            Console.WriteLine("Exception (depth = {0}): {1:F1}x", depth, (double)watch.ElapsedTicks / baselineTime);
         }

         for (var depth = 1; depth <= 10; depth++)
         {
            watch.Restart();
            for (var i = 0; i < NumIterations; i++)
            {
               try
               {
                  ExceptionMethod(depth);
               }
               catch (ArgumentNullException)
               {
               }
               catch (ArgumentException)
               {
               }
               catch (InvalidOperationException)
               {
               }
               catch (Exception)
               {
               }
            }

            watch.Stop();
            Console.WriteLine("Exception (catchlist, depth = {0}): {1:F1}x", depth,
               (double)watch.ElapsedTicks / baselineTime);
         }

         Console.ReadLine();
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      private static void EmptyMethod()
      {
      }

      private static void ExceptionMethod(int depth)
      {
         if (depth > 1)
         {
            ExceptionMethod(depth - 1);
         }
         else
         {
            throw new InvalidOperationException();
         }
      }
   }
}