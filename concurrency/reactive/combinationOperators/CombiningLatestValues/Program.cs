using System.Reactive.Linq;
using System.Reactive.Subjects;
using RxHelpers;

Demo.DisplayHeader(
   "The CombineLatest operator - combines values from the observables whenever any of the observable sequences produces an element");

var heartRate = new Subject<int>();
var speed = new Subject<int>();

speed
   .CombineLatest(heartRate,
      (s, h) => $"Heart:{h} Speed:{s}")
   .SubscribeConsole("Metrics");

heartRate.OnNext(200);
heartRate.OnNext(201);
heartRate.OnNext(202);
speed.OnNext(30);
speed.OnNext(31);
heartRate.OnNext(203);
heartRate.OnNext(204);