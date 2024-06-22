using System.Reactive.Linq;

namespace ObserverContractApproach;

public class Demo : IObserver<Event>
{
   public Demo()
   {
      var person = new Person();
      var sub = person.Subscribe(this);

      person
         .OfType<FallsIllEvent>()
         .Subscribe(args => Console.WriteLine($"A doctor has been called to {args.Address}"));
   }

   public void OnNext(Event value)
   {
      if (value is FallsIllEvent args)
      {
         Console.WriteLine($"A doctor has been called to {args.Address}");
      }
   }

   public void OnError(Exception error)
   {
   }

   public void OnCompleted()
   {
   }
}