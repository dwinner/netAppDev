using System;
using Ninject.Extensions.Interception;
using log4net;

namespace Infrastructure
{
    public class LoggerInterceptorcs : IInterceptor
    {
        private readonly ILog log;

        public LoggerInterceptorcs(ILog log)
        {
            if (log == null)
            {
                throw new ArgumentNullException("log");
            }
            this.log = log;
        }

        public void Intercept(IInvocation invocation)
        {
            log.DebugFormat("Executing {0}...", invocation.Request.Method);
            invocation.Proceed();
            log.DebugFormat("Executed {0}.", invocation.Request.Method);
        }
    }
}