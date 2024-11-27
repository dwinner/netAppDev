using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
   internal static class Program
   {
      private static readonly Stopwatch _Watch = new Stopwatch();
      private static int _PendingTasks;

      private static void Main()
      {
         const int maxValue = 1_000_000_000;

         // Sequential for comparison
         _Watch.Start();
         long sum = 0;
         for (var i = 0; i <= maxValue; i++)
         {
            sum += (long)Math.Sqrt(i);
         }

         _Watch.Stop();

         Console.WriteLine("Sequential: {0}", _Watch.Elapsed);

         _Watch.Restart();
         var numTasks = Environment.ProcessorCount;
         _PendingTasks = numTasks;
         var perThreadCount = maxValue / numTasks;
         var perThreadLeftover = maxValue % numTasks;

         var tasks = new Task<long>[numTasks];

         for (var i = 0; i < numTasks; i++)
         {
            var start = i * perThreadCount;
            var end = (i + 1) * perThreadCount;
            if (i == numTasks - 1)
            {
               end += perThreadLeftover;
            }

            tasks[i] = Task.Run(() =>
            {
               long threadSum = 0;
               for (var j = start; j <= end; j++)
               {
                  threadSum += (long)Math.Sqrt(j);
               }

               return threadSum;
            });
            tasks[i].ContinueWith(OnTaskEnd);
         }

         // You shouldn't normally wait on tasks, but in this case we want to wait 
         // until the previous test is complete
         Task.WaitAll(tasks);

         _Watch.Restart();
         sum = 0;
         Parallel.For(0, maxValue, i => { Interlocked.Add(ref sum, (long)Math.Sqrt(i)); });
         _Watch.Stop();
         Console.WriteLine("Parallel.For: {0}", _Watch.Elapsed);
      }

      private static void OnTaskEnd(Task<long> task)
      {
         Console.WriteLine("Thread sum: {0}", task.Result);
         if (Interlocked.Decrement(ref _PendingTasks) == 0)
         {
            _Watch.Stop();
            Console.WriteLine("Tasks: {0}", _Watch.Elapsed);
         }
      }
   }
}