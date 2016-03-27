using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ErrorHandling
{
   public static class BlockErrorExtensions
   {
      public static void BlockErrorHandler(this IDataflowBlock block, Action<Exception> errorHandler)
      {
         block.Completion.ContinueWith(b =>
         {
            var innerExceptions = block.Completion.Exception?.Flatten().InnerExceptions;
            if (innerExceptions == null)
               return;

            foreach (var error in innerExceptions)
            {
               errorHandler(error);
            }
         }, TaskContinuationOptions.OnlyOnFaulted);
      }

      public static void ForwardError(this IDataflowBlock block, IDataflowBlock destinationBlock)
      {
         block.Completion.ContinueWith(b => destinationBlock.Fault(block.Completion.Exception),
            TaskContinuationOptions.OnlyOnFaulted);
      }
   }
}