// Like TimeStamp but gives the time-interval between successive values

using System.Reactive.Linq;

var observable = Observable.Interval(TimeSpan.FromMilliseconds(750)).TimeInterval();

using (observable.Subscribe(
          x => Console.WriteLine("{0}: {1}", x.Value, x.Interval)))
{
   Console.WriteLine("Press any key to unsubscribe");
   Console.ReadKey();
}

Console.WriteLine("Press any key to exit");
Console.ReadKey();