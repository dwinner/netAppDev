using System.Reactive.Linq;
using RxHelpers;
using Ex = RxHelpers.ObservableEx;

Demo.DisplayHeader(
   "The Window operator - each window is an observable that can be used with an aggregation function");

var donationsWindow1 = Ex.FromValues(50M, 55, 60);
var donationsWindow2 = Ex.FromValues(49M, 48, 45);

var donations =
   donationsWindow1.Concat(donationsWindow2.DelaySubscription(TimeSpan.FromSeconds(1.5)));

var windows = donations.Window(TimeSpan.FromSeconds(1));

var donationsSums =
   from window in windows.Do(_ => Console.WriteLine("New Window"))
   from sum in window.Scan((prevSum, donation) => prevSum + donation)
   select sum;

donationsSums.RunExample("donations in shift");