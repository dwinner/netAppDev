using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TemeletryService;

public class Telemetry(ILoggerFactory loggerFactory)
{
   private readonly ILogger _logger = loggerFactory.CreateLogger<Telemetry>();

   [Function("Telemetry")]
   public void Run([CosmosDBTrigger(
         "carshare-db",
         "car-telemetry",
         Connection = "CosmosDBConnection",
         LeaseContainerName = "leases",
         CreateLeaseContainerIfNotExists = true)]
      IReadOnlyList<CarTelemetry> input)
   {
      if (input != null && input.Count > 0)
      {
         _logger.LogInformation("Documents modified: " + input.Count);
         _logger.LogInformation("First document Id: " + input[0].carid);
      }
   }
}

public class CarTelemetry
{
   public string carid { get; set; }

   public DateTime Date { get; set; }

   public string Data { get; set; }
}