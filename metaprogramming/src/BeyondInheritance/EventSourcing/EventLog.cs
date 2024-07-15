namespace BeyondInheritance.EventSourcing;

public class EventLog(IObservers observers) : IEventLog
{
   private EventSequenceNumber _sequenceNumber = 0;

   public async Task Append(EventSourceId eventSourceId, IEvent @event)
   {
      await observers.OnNext(
         @event,
         new EventContext(eventSourceId, _sequenceNumber, DateTimeOffset.UtcNow));
      _sequenceNumber++;
   }
}