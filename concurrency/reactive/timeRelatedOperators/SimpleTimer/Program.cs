using System.Reactive.Linq;

Console.WriteLine(DateTime.Now);

var observable = Observable.Timer(
   TimeSpan.FromSeconds(5),
   TimeSpan.FromSeconds(1)).Timestamp();

// or, equivalently
// var observable = Observable.Timer(DateTime.Now + TimeSpan.FromSeconds(5), 
//                                                TimeSpan.FromSeconds(1)).Timestamp();

using (observable.Subscribe(
          x => Console.WriteLine("{0}: {1}", x.Value, x.Timestamp)))
{
   Console.WriteLine("Press any key to unsubscribe");
   Console.ReadKey();
}

Console.WriteLine("Press any key to exit");
Console.ReadKey();