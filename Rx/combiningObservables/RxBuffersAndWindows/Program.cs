using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using RxHelpers;
using ObservableEx = RxHelpers.ObservableEx;

namespace RxBuffersAndWindows;

internal static class Program
{
   private static void Main()
   {
      UsingBufferWithAmount();
      BufferingHiRateChatMessages();
      Window();
      AggreagateResultInAWindow();
      ControllingTheWindowClosure();
      Console.WriteLine("Press any key to continue");
      Console.ReadLine();
   }

   private static void UsingBufferWithAmount()
   {
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
   }

   private static void BufferingHiRateChatMessages()
   {
      Demo.DisplayHeader("The Buffer operator - can be used to slow high-rate stream by taking it by chunks");

      var coldMessages = Observable.Interval(TimeSpan.FromMilliseconds(50))
         .Take(4)
         .Select(x => $"Message {x}");

      var messages =
         coldMessages.Concat(
               coldMessages.DelaySubscription(TimeSpan.FromMilliseconds(200)))
            .Publish()
            .RefCount();

      messages.Buffer(messages.Throttle(TimeSpan.FromMilliseconds(100)))
         .SelectMany((b, i) => b.Select(m => $"Buffer {i} - {m}"))
         .RunExample("Hi-Rate Messages");
   }

   private static void Window()
   {
      Demo.DisplayHeader(
         "The Window operator - split the observable sequence into sub-observables along temporal boundaries");

      var numbers = Observable.Interval(TimeSpan.FromMilliseconds(50));
      var windows = numbers.Window(TimeSpan.FromMilliseconds(200));

      windows.Do(_ => Console.WriteLine("New Window:"))
         .Take(3)
         .SelectMany(x => x)
         .SubscribeConsole();
   }

   private static void AggreagateResultInAWindow()
   {
      Demo.DisplayHeader(
         "The Window operator - each window is an observable that can be used with an aggregation function");

      var donationsWindow1 = ObservableEx.FromValues(50M, 55, 60);
      var donationsWindow2 = ObservableEx.FromValues(49M, 48, 45);

      var donations =
         donationsWindow1.Concat(donationsWindow2.DelaySubscription(TimeSpan.FromSeconds(1.5)));

      var windows = donations.Window(TimeSpan.FromSeconds(1));

      var donationsSums =
         from window in windows.Do(_ => Console.WriteLine("New Window"))
         from sum in window.Scan((prevSum, donation) => prevSum + donation)
         select sum;

      donationsSums.RunExample("donations in shift");
   }

   private static void ControllingTheWindowClosure()
   {
      var numbers = new Subject<int>();
      var mouseClicks = new Subject<Unit>();
      numbers.Window(() => mouseClicks);
   }
}