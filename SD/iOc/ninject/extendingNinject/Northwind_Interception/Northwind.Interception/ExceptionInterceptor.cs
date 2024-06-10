using System;
using Ninject.Extensions.Interception;

namespace Northwind.Interception
{
   public class ExceptionInterceptor : IInterceptor
   {
      private readonly IExceptionHandlerService _exceptionHandlerService;

      public ExceptionInterceptor(IExceptionHandlerService handlerService) => _exceptionHandlerService =
         handlerService ?? throw new ArgumentNullException(nameof(handlerService));

      public void Intercept(IInvocation invocation)
      {
         try
         {
            invocation.Proceed();
         }
         catch (Exception exception)
         {
            _exceptionHandlerService.HandleException(exception);
         }
      }
   }
}