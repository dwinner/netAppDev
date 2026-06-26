using System.Text.Json;
using Confluent.Kafka;
using CQRS.Core.Events;
using CQRS.Core.Producers;
using Microsoft.Extensions.Options;

namespace Post.Cmd.Infrastructure.Producers;

public class EventProducer(IOptions<ProducerConfig> config) : IEventProducer
{
   private readonly ProducerConfig _config = config.Value;

   public async Task ProduceAsync<T>(string aTopic, T anEvent) where T : BaseEvent
   {
      using var producer = new ProducerBuilder<string, string>(_config)
         .SetKeySerializer(Serializers.Utf8)
         .SetValueSerializer(Serializers.Utf8)
         .Build();

      var eventMessage = new Message<string, string>
      {
         Key = Guid.NewGuid().ToString(),
         Value = JsonSerializer.Serialize(anEvent, anEvent.GetType())
      };

      var deliveryResult = await producer.ProduceAsync(aTopic, eventMessage);
      if (deliveryResult.Status == PersistenceStatus.NotPersisted)
      {
         throw new Exception(
            $"Could not produce {anEvent.GetType().Name} message to topic - {aTopic} due to the following reason: {deliveryResult.Message}.");
      }
   }
}