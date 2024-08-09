using System.Reactive.Linq;

var source = Observable.Range(1, 10);
var subscription = source.Subscribe(
   x => Console.WriteLine("OnNext: {0}", x),
   ex => Console.WriteLine("OnError: {0}", ex.Message),
   () => Console.WriteLine("OnCompleted"));
Console.WriteLine("Press ENTER to unsubscribe...");
Console.ReadLine();
subscription.Dispose();