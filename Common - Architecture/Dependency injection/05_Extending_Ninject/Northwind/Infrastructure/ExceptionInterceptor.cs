using Ninject.Extensions.Interception;
using System;

namespace Infrastructure
{
    public class ExceptionInterceptor : IInterceptor
    {
        private readonly ExceptionHandlerService exceptionHandlerService;

        public ExceptionInterceptor(ExceptionHandlerService handlerService)
        {
            if (handlerService == null)
            {
                throw new ArgumentNullException("handlerService");
            }
            this.exceptionHandlerService = handlerService;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                exceptionHandlerService.HandleException(exception);
            }
        }
    }
}