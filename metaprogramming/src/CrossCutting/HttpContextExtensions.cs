using Microsoft.AspNetCore.Authorization.Policy;

namespace CrossCutting;

public static class HttpContextExtensions
{
   private const string AuthorizeResultKey = "_AuthorizeResult";

   public static PolicyAuthorizationResult GetAuthorizationResult(this HttpContext context) =>
      (context.Items[AuthorizeResultKey] as PolicyAuthorizationResult)!;

   public static void SetAuthorizationResult(this HttpContext context, PolicyAuthorizationResult result) =>
      context.Items[AuthorizeResultKey] = result;
}