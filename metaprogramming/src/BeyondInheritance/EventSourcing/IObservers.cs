namespace BeyondInheritance.EventSourcing;

public interface IObservers
{
   Task OnNext(IEvent @event, EventContext context);
}