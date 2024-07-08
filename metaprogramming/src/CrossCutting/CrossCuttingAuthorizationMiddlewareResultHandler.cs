using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace CrossCutting;

public class CrossCuttingAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
   private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

   public async Task HandleAsync(
      RequestDelegate next,
      HttpContext context,
      AuthorizationPolicy policy,
      PolicyAuthorizationResult authorizeResult)
   {
      context.SetAuthorizationResult(authorizeResult);
      await _defaultHandler.HandleAsync(next, context, policy, PolicyAuthorizationResult.Success());
   }
}