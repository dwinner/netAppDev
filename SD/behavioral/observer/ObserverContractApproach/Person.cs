namespace ObserverContractApproach;

public class Person : IObservable<Event>
{
   private readonly HashSet<Subscription> _subscriptions = [];

   public IDisposable Subscribe(IObserver<Event> observer)
   {
      var subscription = new Subscription(this, observer);
      _subscriptions.Add(subscription);

      return subscription;
   }

   public void CatchACold()
   {
      foreach (var sub in _subscriptions)
      {
         sub.Observer.OnNext(new FallsIllEvent { Address = "123 London Road" });
      }
   }

   private class Subscription(Person person, IObserver<Event> observer) : IDisposable
   {
      public readonly IObserver<Event> Observer = observer;

      public void Dispose()
      {
         person._subscriptions.Remove(this);
      }
   }
}