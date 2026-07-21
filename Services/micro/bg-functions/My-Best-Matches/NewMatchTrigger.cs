using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace My_Best_Matches;

public class NewMatchTrigger(ILogger<NewMatchTrigger> logger)
{
   [Function(nameof(NewMatchTrigger))]
   public void Run([QueueTrigger("new-match", Connection = "CarSharingStorage")] QueueMessage message)
   {
      logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
   }
}