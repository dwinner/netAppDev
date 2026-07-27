using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlobStorageTrigger;

public class Error(ILogger<Error> logger)
{
   [Function(nameof(Error))]
   public async Task Run([BlobTrigger("samples-workitems-error/{name}", Connection = "Teste")] Stream stream,
      string name)
   {
      using var blobStreamReader = new StreamReader(stream);
      var content = await blobStreamReader.ReadToEndAsync();
      logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
   }
}