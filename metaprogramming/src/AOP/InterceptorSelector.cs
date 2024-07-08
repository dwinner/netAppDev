using System.Reflection;
using Castle.DynamicProxy;

namespace AOP;

public class InterceptorSelector : IInterceptorSelector
{
   public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
   {
      if (type.Namespace?.StartsWith("AOP.Todo", StringComparison.InvariantCulture) ?? false)
      {
         return interceptors;
      }

      return interceptors.Where(interceptor => interceptor.GetType() != typeof(AuthorizationInterceptor)).ToArray();
   }
}