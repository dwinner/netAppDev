using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ActorPingPong;

public class Ping : ISubject<Pong, Ping>
{
   public void OnCompleted()
   {
      Console.WriteLine("Ping finished.");
   }

   public void OnError(Exception error)
   {
      Console.WriteLine("Ping experienced an exception and had to quit playing.");
   }

   public void OnNext(Pong value)
   {
      Console.WriteLine("Ping received Pong.");
   }

   public IDisposable Subscribe(IObserver<Ping> observer) =>
      Observable.Interval(TimeSpan.FromSeconds(2))
         .Where(n => n < 10)
         .Select(_ => this)
         .Subscribe(observer);

   public void Dispose()
   {
      OnCompleted();
   }
}