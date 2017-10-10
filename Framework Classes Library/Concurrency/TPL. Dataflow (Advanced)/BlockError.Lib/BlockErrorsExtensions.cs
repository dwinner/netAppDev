using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace BlockError.Lib
{
   // ReSharper disable UnusedMember.Global
   public static class BlockErrorsExtensions      
   {
      public static void BlockErrorHandler(this IDataflowBlock @this, Action<Exception> errorHandler)
         => @this.Completion.ContinueWith(_ =>
         {
            var innerExceptions = @this.Completion.Exception?.Flatten().InnerExceptions;
            if (innerExceptions != null)
               foreach (var error in innerExceptions)
                  errorHandler(error);
         }, TaskContinuationOptions.OnlyOnFaulted);

      public static void ForwardError(this IDataflowBlock @this, IDataflowBlock destinationBlock)
         => @this.Completion.ContinueWith(_ => destinationBlock.Fault(@this.Completion.Exception),
            TaskContinuationOptions.OnlyOnFaulted);
   }
   // ReSharper restore UnusedMember.Global
}