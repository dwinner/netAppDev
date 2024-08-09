using System.Reactive;
using System.Reactive.Linq;
using RxHelpers;

Demo.DisplayHeader("Schedule single emission with Timer (relative)----");

Console.WriteLine("Scheduling to 5 sec from subscription");
var observable = Observable
   .Timer(TimeSpan.FromSeconds(5))
   .Timestamp();

Console.WriteLine("subscribing at {0}", new Timestamped<string>(string.Empty, DateTimeOffset.UtcNow));
observable.RunExample("Timer (relative)");