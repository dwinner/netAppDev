using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace dcnm;

public class Health(ILogger<Health> logger)
{
   [Function("Health")]
   public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
   {
      logger.LogInformation("HTTP trigger function for Health processed a request.");
      return new OkObjectResult(
         $"Yes! The Health Function is live!. UTC: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
   }
}