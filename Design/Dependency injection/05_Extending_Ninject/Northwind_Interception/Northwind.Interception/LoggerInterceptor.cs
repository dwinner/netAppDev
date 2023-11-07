using System;
using log4net;
using Ninject.Extensions.Interception;

namespace Northwind.Interception
{
   public class LoggerInterceptor : IInterceptor
   {
      private readonly ILog _log;

      public LoggerInterceptor(ILog log) => _log = log ?? throw new ArgumentNullException(nameof(log));

      public void Intercept(IInvocation invocation)
      {
         _log.DebugFormat("Executing {0}...", invocation.Request.Method);
         invocation.Proceed();
         _log.DebugFormat("Executed {0}.", invocation.Request.Method);
      }
   }
}