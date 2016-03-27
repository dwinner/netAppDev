/**
 * Корректное уведомление о завершении узлов потока данных
 */

using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace ShuttingDown
{
   internal static class Program
   {
      private static void Main()
      {
         SingleBlockShutdown();
         PropagatingCompletion();
      }

      private static void PropagatingCompletion()
      {
         var firstBlock = new TransformBlock<int, int>(i => i * 2);
         var secondBlock = new ActionBlock<int>(i =>
         {
            Thread.Sleep(500);
            Console.WriteLine(i);
         });

         var thirdBlock = new ActionBlock<int>((Action<int>)Console.WriteLine);

         firstBlock.LinkTo(secondBlock, new DataflowLinkOptions { PropagateCompletion = true });
         firstBlock.LinkTo(thirdBlock, new DataflowLinkOptions { PropagateCompletion = true });

         for (var i = 0; i < 10; i++)
         {
            firstBlock.Post(i);
         }

         firstBlock.Complete(); // firstBlock.Completion.Wait();         
         secondBlock.Completion.Wait(); //secondBlock.Complete();

         //Task.WaitAll(new Task[] {secondBlock.Completion, thirdBlock.Completion});
      }

      private static void SingleBlockShutdown()
      {
         var actionBlock = new ActionBlock<int>((Action<int>)Console.WriteLine);
         for (var i = 0; i < 10; i++)
         {
            actionBlock.Post(i);
         }

         Console.WriteLine("Completing..");
         actionBlock.Complete();
         Console.WriteLine("Waiting..");
         actionBlock.Completion.Wait();
         Console.WriteLine(actionBlock.Completion.Status);
      }
   }
}