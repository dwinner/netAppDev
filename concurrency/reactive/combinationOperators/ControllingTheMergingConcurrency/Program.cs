using System.Reactive.Linq;
using RxHelpers;

Demo.DisplayHeader(
   "The Merge operator - you can control the amount of concurrent subscription made by Merge ");

var first = Observable.Interval(TimeSpan.FromSeconds(1)).Select(i => $"First{i}").Take(2);
var second = Observable.Interval(TimeSpan.FromSeconds(1)).Select(i => $"Second{i}").Take(2);
var third = Observable.Interval(TimeSpan.FromSeconds(1)).Select(i => $"Third{i}").Take(2);
new[] { first, second, third }.ToObservable()
   .Merge(2)
   .RunExample("Merge with 2 concurrent subscriptions");