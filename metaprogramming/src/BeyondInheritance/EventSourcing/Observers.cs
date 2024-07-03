using Fundamentals;

namespace BeyondInheritance.EventSourcing;

[Singleton]
public class Observers : IObservers
{
   private readonly IEnumerable<ObserverHandler> _handlers;

   public Observers(ITypes types, IServiceProvider serviceProvider) =>
      _handlers = types.All
         .Where(type => type.HasAttribute<ObserverAttribute>())
         .Select(targetType => new ObserverHandler(serviceProvider, targetType));

   public Task OnNext(IEvent @event, EventContext context)
   {
      var tasks = _handlers
         .Where(hdl => hdl.EventTypes.Contains(@event.GetType()))
         .Select(hdl => hdl.OnNext(@event, context));
      return Task.WhenAll(tasks);
   }
}