using System.Reactive.Linq;
using PeriodicallyGetUpdates;
using RxHelpers;

Demo.DisplayHeader("Using Interval to periodically poll a webservice");

var updatesWebService = new UpdatesWebService();
var observable = Observable
   .Interval(TimeSpan.FromSeconds(3))
   .Take(3) // we don't want to run the example forever, so we'll do only 3 updates
   .SelectMany(_ => updatesWebService.GetUpdatesAsync())
   .SelectMany(updates => updates);

observable.RunExample("updates");