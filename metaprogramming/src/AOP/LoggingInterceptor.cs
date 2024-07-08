using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace AOP;

public class LoggingInterceptor(ILoggerFactory loggerFactory) : IInterceptor
{
   public void Intercept(IInvocation invocation)
   {
      var logger = loggerFactory.CreateLogger(invocation.TargetType);
      logger.BeforeInvocation(invocation.Method.Name);

      try
      {
         invocation.Proceed();
         if (invocation.ReturnValue is Task task)
         {
            task.ContinueWith(lTask =>
            {
               if (lTask.IsFaulted)
               {
                  logger.InvocationError(invocation.Method.Name, lTask.Exception!);
               }
               else
               {
                  logger.AfterInvocation(invocation.Method.Name);
               }
            });
         }
         else
         {
            logger.AfterInvocation(invocation.Method.Name);
         }
      }
      catch (Exception ex)
      {
         logger.InvocationError(invocation.Method.Name, ex);
         throw;
      }
   }
}