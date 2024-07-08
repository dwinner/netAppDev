using Castle.DynamicProxy;

namespace AOP;

public class AuthorizationInterceptor(IUsersServiceComposition usersService) : IInterceptor
{
   public void Intercept(IInvocation invocation)
   {
      if (usersService.IsAuthorized("jane@doe.io", invocation.Method.Name))
      {
         invocation.Proceed();
      }
   }
}