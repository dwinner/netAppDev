using System.Reactive.Linq;
using RxHelpers;

Demo.DisplayHeader(
   "Creating observable with Timer - emission will start 5 sec after the subscription, with a period of 1 sec between notification----");

var observable = Observable
   .Timer(TimeSpan.FromSeconds(5), //first emission
      TimeSpan.FromSeconds(1))
   .Take(5); // we don't want to run the example forever, so we'll do only 3 emissions

Console.WriteLine("subscribing");
observable.RunExample("Timer(5s,1s)");