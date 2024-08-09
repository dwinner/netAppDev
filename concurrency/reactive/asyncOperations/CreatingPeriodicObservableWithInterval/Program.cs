using System.Reactive.Linq;
using RxHelpers;

Demo.DisplayHeader("Creating observable with Interval");

var observable = Observable
   .Interval(TimeSpan.FromSeconds(1))
   .Take(5); // we don't want to run the example forever, so we'll do only 3 emissions

observable.RunExample("Interval");