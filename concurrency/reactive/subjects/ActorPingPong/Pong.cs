using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ActorPingPong;

public class Pong : ISubject<Ping, Pong>
{
   public void OnCompleted()
   {
      Console.WriteLine("Pong finished.");
   }

   public void OnError(Exception error)
   {
      Console.WriteLine("Pong experienced an exception and had to quit playing.");
   }

   public void OnNext(Ping value)
   {
      Console.WriteLine("Pong received Ping.");
   }

   public IDisposable Subscribe(IObserver<Pong> observer) =>
      Observable.Interval(TimeSpan.FromSeconds(1.5))
         .Where(n => n < 10)
         .Select(n => this)
         .Subscribe(observer);

   public void Dispose()
   {
      OnCompleted();
   }
}