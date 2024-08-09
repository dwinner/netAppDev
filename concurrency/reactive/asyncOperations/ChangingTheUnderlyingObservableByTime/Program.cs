using System.Reactive.Linq;
using RxHelpers;

Demo.DisplayHeader("Switching observables after 5 seconds (with timer)");

var firstObservable =
   Observable
      .Interval(TimeSpan.FromSeconds(1))
      .Select(x => $"first{x}");
var secondObservable =
   Observable
      .Interval(TimeSpan.FromSeconds(2))
      .Select(x => $"second{x}")
      .Take(5); // we don't want to run the example forever, so we'll do only 5 emission;

var immediateObservable = Observable.Return(firstObservable);

//Scheduling the second observable emission
var scheduledObservable =
   Observable
      .Timer(TimeSpan.FromSeconds(5))
      .Select(_ => secondObservable);

var switchingObservable = immediateObservable
   .Merge(scheduledObservable)
   .Switch()
   .Timestamp();

Console.WriteLine("subscribing");
switchingObservable.RunExample("timer switch");