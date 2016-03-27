/**
 * Реализация идиомы поставщика/потребителя через потоки данных
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using static System.Console;

namespace ProducerConsumerDataFlow
{
   internal static class Program
   {
      private static void Main()
      {
         //LazyProducerConsumer();

         var blockConfiguration = new ExecutionDataflowBlockOptions
         {
            NameFormat = "Type:{0},Id:{1}",
            MaxDegreeOfParallelism = 2,
            BoundedCapacity = 2
         };

         var consumerBlock = new ActionBlock<int>(new Action<int>(SlowConsumer), blockConfiguration);

         for (var i = 0; i < 5; i++)
         {
            consumerBlock.Post(i);
            // Console.WriteLine("Sending {0}",i);
            // consumerBlock.SendAsync(i).Wait();
         }

         WriteLine(consumerBlock.ToString());

         consumerBlock.Complete();
         consumerBlock.Completion.Wait();
      }

      private static void SlowConsumer(int val)
      {
         WriteLine("{0}: Consuming {1}", Task.CurrentId, val);
         Thread.Sleep(1000);
      }

      /*

      private static void LazyProducerConsumer()
      {
         var consumerBlock = new ActionBlock<int>(new Action<int>(Consume));
         PrintThreadPoolUsage("Main");
         for (var i = 0; i < 5; i++)
         {
            consumerBlock.Post(i);
            Thread.Sleep(1000);
            PrintThreadPoolUsage("loop");
         }

         consumerBlock.Complete();
         consumerBlock.Completion.Wait();
      }

      private static void Consume(int val)
      {
         PrintThreadPoolUsage("Consume");
         WriteLine("{0}:{1} is thread pool thread {2}", Task.CurrentId, val,
            Thread.CurrentThread.IsThreadPoolThread);
      }      

      private static void PrintThreadPoolUsage(string label)
      {
         int cpu;
         int io;
         ThreadPool.GetAvailableThreads(out cpu, out io);
         WriteLine("{0}:CPU:{1},IO:{2}", label, cpu, io);
      }

      */
   }
}