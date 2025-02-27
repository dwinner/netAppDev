using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CrossCutting;

public class AdminForNamespaceHandler : AuthorizationHandler<AdminForNamespace>
{
   protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminForNamespace requirement)
   {
      if (context.Resource is not HttpContext httpContext)
      {
         return Task.CompletedTask;
      }

      var endpoint = httpContext.GetEndpoint();
      if (endpoint is null)
      {
         return Task.CompletedTask;
      }

      var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
      if (controllerActionDescriptor?
             .MethodInfo
             .DeclaringType?
             .Namespace?
             .StartsWith(
                requirement.Namespace, StringComparison.InvariantCulture) == true
          && !httpContext.User.IsInRole("Admin"))
      {
         context.Fail();
      }
      else
      {
         context.Succeed(requirement);
      }

      return Task.CompletedTask;
   }
}