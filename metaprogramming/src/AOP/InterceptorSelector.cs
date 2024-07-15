using System.Reflection;
using Castle.DynamicProxy;

namespace AOP;

public class InterceptorSelector : IInterceptorSelector
{
   private const string ValidNsPrefix = "AOP.Todo";

   public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors) =>
      type.Namespace?.StartsWith(ValidNsPrefix, StringComparison.InvariantCulture) ?? false
         ? interceptors
         : interceptors.Where(interceptor => interceptor.GetType() != typeof(AuthorizationInterceptor)).ToArray();
}