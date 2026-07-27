using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace RoutesPlanner;

public class CarSeeking(ILogger<CarSeeking> logger)
{
   [Function(nameof(CarSeeking))]
   public async Task Run(
      [ServiceBusTrigger("car-seeking-requests", "routes", Connection = "car-share-bus")]
      ServiceBusReceivedMessage message,
      ServiceBusMessageActions messageActions)
   {
      logger.LogInformation("Message ID: {id}", message.MessageId);
      logger.LogInformation("Message Body: {body}", message.Body);
      logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

      // Complete the message
      await messageActions.CompleteMessageAsync(message);
   }
}