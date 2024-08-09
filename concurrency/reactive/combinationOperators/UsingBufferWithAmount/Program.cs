using System.Reactive.Linq;
using RxHelpers;

Demo.DisplayHeader("The Buffer operator - gather items from an Observable into bundles.");

var speedReadings = new[] { 50.0, 51.0, 51.5, 53.0, 52.0, 52.5, 53.0 } //in MPH
   .ToObservable();

const double timeDelta = 0.0002777777777777778; //1 second in hours unit

var accelrations =
   from buffer in speedReadings.Buffer(2, 1)
   where buffer.Count == 2
   let speedDelta = buffer[1] - buffer[0]
   select speedDelta / timeDelta;

accelrations.RunExample("Acceleration");