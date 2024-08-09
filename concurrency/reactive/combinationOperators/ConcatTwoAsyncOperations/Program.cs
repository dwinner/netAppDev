using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using RxHelpers;

Demo.DisplayHeader(
   "The Concat operator - Concatenates the second observable sequence to the first observable sequence upon successful termination of the first");

var facebookMessages =
   Task.Delay(10).ContinueWith(_ => new[] { "Facebook1", "Facebook2" }); //this will finish after 10 millis
var twitterStatuses =
   Task.FromResult(new[] { "Twitter1", "Twitter2" }); //this will finish immediately

facebookMessages.ToObservable().Concat(twitterStatuses.ToObservable())
   .SelectMany(messages => messages)
   .RunExample("Concat Messages");