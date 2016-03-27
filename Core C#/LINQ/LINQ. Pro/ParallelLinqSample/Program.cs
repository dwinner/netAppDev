/**
 * Планирование выполнения запросов в параллельном режиме
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLinqSample
{
   static class Program
   {
      static void Main()
      {
         IntroParallel();
         Cancellation();
      }

      private static IEnumerable<int> SampleData()
      {
         const int arraySize = 100000000;
         var r = new Random();
         return Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();
      }

      private static void IntroParallel()
      {
         var data = SampleData();
         IEnumerable<int> source = data as IList<int> ?? data.ToList();

         var watch = new Stopwatch();
         watch.Start();

         double seqQuery = (from x in source
                            where Math.Log(x) < 4
                            select x).Average();
         watch.Stop();

         Console.WriteLine("Sync {0}, result: {1}", watch.ElapsedMilliseconds, seqQuery);

         watch.Reset();
         watch.Start();
         double parQuery = (from x in Partitioner.Create(source).AsParallel()
                            where Math.Log(x) < 4
                            select x).Average();
         watch.Stop();
         Console.WriteLine("Async {0}, result: {1}", watch.ElapsedMilliseconds, parQuery);
      }

      private static void Cancellation()
      {
         IEnumerable<int> sampleData = SampleData();

         var tokenSource = new CancellationTokenSource();

         Task.Factory.StartNew(() =>
         {
            try
            {
               double result = (from source in sampleData.AsParallel().WithCancellation(tokenSource.Token)
                                where Math.Log(source) < 4
                                select source).Average();
               Console.WriteLine("Query finished, result: {0}", result);
            }
            catch (OperationCanceledException ex)
            {
               Console.WriteLine(ex.Message);
            }
         }, tokenSource.Token);

         Console.WriteLine("Query started");
         Console.Write("Cancel?");
         string input = Console.ReadLine();
         if (input != null && input.ToLower().Equals("y"))
         {
            tokenSource.Cancel();
            Console.WriteLine("Sent a cancel");
         }

         Console.WriteLine("Press return to exit");
         Console.ReadLine();
      }
   }
}
