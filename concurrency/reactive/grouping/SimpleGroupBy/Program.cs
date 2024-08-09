using System.Reactive.Linq;

namespace SimpleGroupBy;

internal static class Program
{
   private static void Main()
   {
      var timeToStop = new ManualResetEvent(false);
      var keyPresses = KeyPresses().ToObservable();

      var groupedKeyPresses =
         from keyInfo in keyPresses
         group keyInfo by keyInfo.Key
         into keyPressGroup
         select keyPressGroup;

      Console.WriteLine("Press Enter to stop.  Now bang that keyboard!");

      groupedKeyPresses.Subscribe(keyPressGroup =>
      {
         var numberPresses = 0;
         keyPressGroup.Subscribe(keyPress =>
            {
               Console.WriteLine(
                  "You pressed the {0} key {1} time(s)!",
                  keyPress.Key,
                  ++numberPresses);
            },
            () => timeToStop.Set());
      });

      timeToStop.WaitOne();
   }

   private static IEnumerable<ConsoleKeyInfo> KeyPresses()
   {
      for (;;)
      {
         var currentKey = Console.ReadKey(true);
         if (currentKey.Key == ConsoleKey.Enter)
         {
            yield break;
         }

         yield return currentKey;
      }
   }
}