using System.Reactive.Linq;
using RxHelpers;
using ObservableEx = RxHelpers.ObservableEx;

Demo.DisplayHeader("The Zip operator - combines values with the same index from two observables");

// temperatures from two sensors (in Celsius)
var temp1 = ObservableEx.FromValues(20.0, 21, 22);
var temp2 = ObservableEx.FromValues(22.0, 21, 24);

temp1
   .Zip(temp2, (t1, t2) => (t1 + t2) / 2)
   .SubscribeConsole("Avg Temp.");