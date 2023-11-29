using System.Reactive.Linq;
using RxHelpers;

namespace RxNonStandardEvents;

internal static class Program
{
   private static void Main()
   {
      ConvertingNonStandardEvents();
      ConvertingEventsWithNoArguments();
   }

   private static void ConvertingNonStandardEvents()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Converting Non Standard Events");

      var wifiScanner = new WifiScanner();

      //
      // this code snippet will result in an ArgumentException, since the NetworkFoundEventHandler delegate is not convertible to the standard EventHandler<TEventArgs> type
      //
      //var networks = Observable.FromEventPattern<NetworkFoundEventHandler, string>(
      //      h => wifiScanner.NetworkFound += h,
      //      h => wifiScanner.NetworkFound -= h);
      //networks.SubscribeConsole();

      //
      // when the target event has only one parameter, its easy to make it into observable
      //
      //IObservable<string> networks = Observable.FromEvent<NetworkFoundEventHandler, string>(
      //      h => wifiScanner.NetworkFound += h,
      //      h => wifiScanner.NetworkFound -= h);

      //
      // When the target event has more than one parameter, a conversion method is needed to turn them into a single object
      //
      var networks = Observable
         .FromEvent<ExtendedNetworkFoundEventHandler, Tuple<string, int>>(
            rxHandler =>
               (ssid, strength) => rxHandler(Tuple.Create(ssid, strength)),
            h => wifiScanner.ExtendedNetworkFound += h,
            h => wifiScanner.ExtendedNetworkFound -= h);


      networks.SubscribeConsole();
      networks.SubscribeConsole();

      while (true)
      {
         Console.WriteLine("Enter the network ssid or X to exit");
         var ssid = Console.ReadLine();
         if (ssid == "X")
         {
            break;
         }

         Console.WriteLine("Enter the network strength - 1 to 10");
         var strength = int.Parse(Console.ReadLine());

         wifiScanner.RaiseFound(ssid, strength);
      }
   }

   private static void ConvertingEventsWithNoArguments()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Converting Events With No Arguments");

      var wifiScanner = new WifiScanner();
      var connected = Observable.FromEvent(
         h => wifiScanner.Connected += h,
         h => wifiScanner.Connected -= h);

      connected.SubscribeConsole("connected");

      wifiScanner.RaiseConnected();
      wifiScanner.RaiseConnected();
   }
}

public delegate void NetworkFoundEventHandler(string ssid);

public delegate void ExtendedNetworkFoundEventHandler(string ssid, int strength);