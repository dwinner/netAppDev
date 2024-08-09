using System.Globalization;
using System.Reactive.Linq;

var mainSequence = Observable.Interval(TimeSpan.FromSeconds(1));
var seqWindowed = mainSequence.Window(() =>
{
   var seqWindowControl = Observable.Interval(TimeSpan.FromSeconds(6));
   return seqWindowControl;
});

seqWindowed.Subscribe(seqWindow =>
{
   Console.WriteLine("\nA new window into the main sequence has opened: {0}\n",
      DateTime.Now.ToString(CultureInfo.InvariantCulture));
   seqWindow.Subscribe(x => { Console.WriteLine("Integer : {0}", x); });
});

Console.ReadLine();