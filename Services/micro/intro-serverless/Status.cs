using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace dcnm;

public class Status(ILogger<Status> logger)
{
   [Function("Status")]
   public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
   {
      logger.LogInformation("HTTP trigger function for Status processed a request.");
      return new OkObjectResult(
         $"The Status Function is working properly!. UTC: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
   }
}