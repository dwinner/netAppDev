using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace BlockError.Lib
{
   public static class BlockErrorsExtensions
   {
      public static void BlockErrorHandler(this IDataflowBlock @this, Action<Exception> errorHandler)
         => @this.Completion.ContinueWith(_ =>
         {
            foreach (var error in @this.Completion.Exception.Flatten().InnerExceptions)
               errorHandler(error);
         }, TaskContinuationOptions.OnlyOnFaulted);

      public static void ForwardError(this IDataflowBlock @this, IDataflowBlock destinationBlock)
         => @this.Completion.ContinueWith(_ => destinationBlock.Fault(@this.Completion.Exception),
            TaskContinuationOptions.OnlyOnFaulted);
   }
}