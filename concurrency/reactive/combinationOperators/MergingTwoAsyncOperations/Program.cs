using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using RxHelpers;

Demo.DisplayHeader(
   "The Merge operator - merges the notifications from the source observables into a single observable sequence");

var facebookMessages =
   Task.Delay(10).ContinueWith(_ => new[] { "Facebook1", "Facebook2" }); //this will finish after 10 millis
var twitterStatuses = Task.FromResult(new[] { "Twitter1", "Twitter2" }); //this will finish immediately

facebookMessages
   .ToObservable()
   .Merge(twitterStatuses.ToObservable())
   .SelectMany(messages => messages)
   .RunExample("Merged Messages");