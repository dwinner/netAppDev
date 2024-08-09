using System.Reactive.Linq;
using RxHelpers;
using Ex = RxHelpers.ObservableEx;

Demo.DisplayHeader(
   "The Merge operator - allows also to merge observables emitted from another observable");

var texts = Ex.FromValues("Hello", "World");
texts
   .Select(txt => Observable.Return($"{txt}-Result"))
   .Merge()
   .SubscribeConsole("Merging from observable");