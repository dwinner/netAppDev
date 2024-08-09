using System.Reactive.Linq;
using System.Reactive.Subjects;
using RxHelpers;

Demo.DisplayHeader(
   "The Switch operator - takes an observable that emits observables and creates a single observable that emits the notification from the most recent observable");

var textsSubject = new Subject<string>();
var texts = textsSubject.AsObservable();
texts
   .Select(txt =>
      Observable.Return($"{txt}-Result")
         .Delay(TimeSpan.FromMilliseconds(txt == "R" ? 10 : 0))) //adding delay to R results
   .Switch()
   .SubscribeConsole("Merging from observable");

textsSubject.OnNext("R");
textsSubject.OnNext("Rx");
Thread.Sleep(20); //adding a short delay so the system will have time to process Rx results
textsSubject.OnNext("RX");

Thread.Sleep(20); //short delay so we could see the printouts before moving to the next example