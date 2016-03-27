/**
 * Транслирующий блок
 */

using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace Broadcasting
{
   internal static class Program
   {
      private static void Main()
      {
         var nonGreedy = new ExecutionDataflowBlockOptions { BoundedCapacity = 1 };
         var greedy = new ExecutionDataflowBlockOptions { BoundedCapacity = DataflowBlockOptions.Unbounded };

         var source = new BroadcastBlock<int>(i => i);

         var consumeOne = new ActionBlock<int>((Action<int>)ConsumerOne, nonGreedy);
         var consumeTwo = new ActionBlock<int>((Action<int>)ConsumerTwo, greedy);

         source.LinkTo(consumeOne);
         source.LinkTo(consumeTwo);

         for (var j = 0; j < 10; j++)
         {
            source.Post(j);
            Thread.Sleep(50);
         }
         Console.ReadLine();
      }

      private static void ConsumerTwo(int obj)
      {
         Console.WriteLine("Consumer two {0}", obj);
         Thread.Sleep(60);
      }

      private static void ConsumerOne(int obj)
      {
         Console.WriteLine("Consumer one {0}", obj);
         Thread.Sleep(100);
      }
   }
}