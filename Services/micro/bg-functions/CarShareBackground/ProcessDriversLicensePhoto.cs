using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CarShareBackground;

public class ProcessDriversLicensePhoto(ILoggerFactory loggerFactory)
{
   private readonly ILogger _logger = loggerFactory.CreateLogger<ProcessDriversLicensePhoto>();

   [Function(nameof(ProcessDriversLicensePhoto))]
   public async Task Run(
      [BlobTrigger("drivers-license/{name}", Connection = "CarShareStorage")] Stream myBlob,
      string name)
   {
      var reader = new StreamReader(myBlob);
      var message = await reader.ReadToEndAsync();
      _logger.LogInformation("File detected");
   }
}