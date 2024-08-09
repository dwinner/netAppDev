using System.Reactive;
using System.Reactive.Linq;
using RxHelpers;

Demo.DisplayHeader("Schedule single emission with Timer (absolute)----");

var dateTimeOffset = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(5);

Console.WriteLine("Scheduling to {0}", dateTimeOffset);
var observable = Observable.Timer(dateTimeOffset)
   .Timestamp();

Console.WriteLine("subscribing at {0}", new Timestamped<string>(string.Empty, DateTimeOffset.UtcNow));
observable.RunExample("Timer (relative)");