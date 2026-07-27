using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlobStorageTrigger;

public class FunctionBlobTrigger(ILogger<FunctionQueue> logger)
{
   [Function(nameof(FunctionBlobTrigger))]
   public async Task Run(
      [BlobTrigger("samples-workitems/{name}", Connection = "StorageConnection")] Stream myBlob,
      string name)
   {
      try
      {
         using (var reader = new StreamReader(myBlob))
         {
            var message = reader.ReadToEnd();
            logger.LogInformation("File detected");
         }
      }
      catch (Exception error)
      {
         logger.LogInformation(error.Message);
      }
   }
}