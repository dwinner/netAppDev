using System.Reactive.Linq;
using RxHelpers;

Demo.DisplayHeader("The Amb operator - picks the first observable to emit");

var server1 = Observable.Interval(TimeSpan.FromSeconds(2)).Select(i => $"Server1-{i}");
var server2 = Observable.Interval(TimeSpan.FromSeconds(1)).Select(i => $"Server2-{i}");

server1
   .Amb(server2)
   .Take(3)
   .RunExample("Amb");

//The same could have been written like:
// server1.Amb(server2).Take(3).RunExample("Amb");