using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;

namespace Northwind.Interception
{
   public class LogAttribute : InterceptAttribute
   {
      public override IInterceptor CreateInterceptor(IProxyRequest request) => request.Kernel.Get<LoggerInterceptor>();
   }
}