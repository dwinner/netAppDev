using CQRS.Core.Events;

namespace CQRS.Core.Producers;

public interface IEventProducer
{
   Task ProduceAsync<T>(string aTopic, T anEvent) where T : BaseEvent;
}