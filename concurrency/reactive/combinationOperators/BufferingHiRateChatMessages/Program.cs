using System.Reactive.Linq;
using RxHelpers;

Demo.DisplayHeader("The Buffer operator - can be used to slow high-rate stream by taking it by chunks");

var coldMessages = Observable.Interval(TimeSpan.FromMilliseconds(50))
   .Take(4)
   .Select(x => $"Message {x}");

var messages =
   coldMessages.Concat(
         coldMessages.DelaySubscription(TimeSpan.FromMilliseconds(200)))
      .Publish()
      .RefCount();

messages
   .Buffer(messages.Throttle(TimeSpan.FromMilliseconds(100)))
   .SelectMany((b, i) => b.Select(m => $"Buffer {i} - {m}"))
   .RunExample("Hi-Rate Messages");