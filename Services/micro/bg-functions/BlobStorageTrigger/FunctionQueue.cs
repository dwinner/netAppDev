using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlobStorageTrigger;

public class FunctionQueue(ILogger<FunctionQueue> logger)
{
   [Function(nameof(FunctionQueue))]
   public void Run([QueueTrigger("myqueue-items", Connection = "StorageConnection")] QueueMessage message)
   {
      logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
   }
}