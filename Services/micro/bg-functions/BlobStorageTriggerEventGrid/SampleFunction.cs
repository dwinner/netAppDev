using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlobStorageTriggerEventGrid;

public class SampleFunction(ILogger<SampleFunction> logger)
{
   [Function(nameof(SampleFunction))]
   public async Task Run(
      [BlobTrigger("event-grid-samples/{name}", Source = BlobTriggerSource.EventGrid,
         Connection = "ConnectionStringName")]
      Stream stream, string name)
   {
      using var blobStreamReader = new StreamReader(stream);
      var content = await blobStreamReader.ReadToEndAsync();
      logger.LogInformation($"C# Blob Trigger (using Event Grid) processed blob\n Name: {name} \n Data: {content}");
   }
}