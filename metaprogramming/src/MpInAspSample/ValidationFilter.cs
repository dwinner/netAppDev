using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MpInAspSample;

public class ValidationFilter : IAsyncActionFilter
{
   public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
   {
      if (context.ModelState.IsValid)
      {
         await next();
      }
      else
      {
         context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState));
      }
   }
}