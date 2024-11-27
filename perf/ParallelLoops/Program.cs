using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoops
{
   internal static class Program
   {
      private static void Main()
      {
         var watch = new Stopwatch();
         const int maxValue = 1_000_000_000;

         // Naive For-loop
         watch.Restart();
         long sum = 0;
         Parallel.For(0, maxValue, i => { Interlocked.Add(ref sum, (long)Math.Sqrt(i)); });
         watch.Stop();
         Console.WriteLine("Parallel.For: {0}", watch.Elapsed);

         // Partitioned For-loop
         var partitioner = Partitioner.Create(0, maxValue);
         watch.Restart();
         sum = 0;
         Parallel.ForEach(partitioner,
            range =>
            {
               long partialSum = 0;
               for (var i = range.Item1; i < range.Item2; i++)
               {
                  partialSum += (long)Math.Sqrt(i);
               }

               Interlocked.Add(ref sum, partialSum);
            });
         watch.Stop();
         Console.WriteLine("Partitioned Parallel.For: {0}", watch.Elapsed);
      }
   }
}