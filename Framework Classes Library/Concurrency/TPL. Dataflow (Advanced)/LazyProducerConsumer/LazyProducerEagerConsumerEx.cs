/**
 * Lazy producer consumer
 */

using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using static System.Console;
using static System.Threading.ThreadPool;

namespace _01_LazyProducerConsumer
{
   internal static class LazyProducerEagerConsumerEx
   {
      private static void Main()
      {
         var consumerBlock = new ActionBlock<int>(index => Consume(index));
         PrintThreadPoolUsage(nameof(Main));

         for (var i = 0; i < 5; i++)
         {
            consumerBlock.Post(i);
            Thread.Sleep(1000);
            PrintThreadPoolUsage("loop");
         }

         // Tell the block no more items will be coming
         consumerBlock.Complete();

         // Wait for the block to shutdown
         consumerBlock.Completion.Wait();
      }

      private static void Consume(int val)
      {
         PrintThreadPoolUsage(nameof(Consume));
         WriteLine("{0}:{1} is thread pool thread {2}",
            Task.CurrentId,
            val,
            Thread.CurrentThread.IsThreadPoolThread);
      }

      private static void PrintThreadPoolUsage(string label)
      {
         GetAvailableThreads(out var cpu, out var io);
         WriteLine("{0}:CPU:{1},IO:{2}", label, cpu, io);
      }
   }
}