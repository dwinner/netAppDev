using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using RxHelpers;

Demo.DisplayHeader(
   "The Window operator - split the observable sequence into sub-observables along temporal boundaries");

var numbers = Observable.Interval(TimeSpan.FromMilliseconds(50));
var windows = numbers.Window(TimeSpan.FromMilliseconds(200));

windows
   .Do(_ => Console.WriteLine("New Window:"))
   .Take(3)
   .SelectMany(x => x)
   .SubscribeConsole();

// Controlling the window closure
var nums = new Subject<int>();
var mouseClicks = new Subject<Unit>();
nums.Window(() => mouseClicks);