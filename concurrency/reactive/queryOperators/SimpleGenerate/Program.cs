using System.Reactive.Linq;

var observable = Observable.Generate(1, x => x < 6, x => x + 1, x => x,
   x => TimeSpan.FromSeconds(1)).Timestamp();

using (observable.Subscribe(x => Console.WriteLine("{0}, {1}", x.Value, x.Timestamp)))
{
   Console.WriteLine("Press any key to unsubscribe");
   Console.ReadKey();
}

Console.WriteLine("Press any key to exit");
Console.ReadKey();