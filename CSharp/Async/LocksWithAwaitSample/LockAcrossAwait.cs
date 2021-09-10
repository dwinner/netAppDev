using System;
using System.Threading.Tasks;

namespace LocksWithAwaitSample
{
   internal static class LockAcrossAwait
   {
      private static readonly AsyncSemaphore AsyncSemaphore = new AsyncSemaphore();

      private static async Task Main()
      {
         string[] messages =
         {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six"
         };
         var tasks = new Task[messages.Length];

         for (var i = 0; i < messages.Length; i++)
         {
            var message = messages[i];
            tasks[i] = Task.Run(async () => await LockWithSemaphoreAsync(message));
         }

         await Task.WhenAll(tasks).ConfigureAwait(false);
         Console.WriteLine();
      }

      private static async Task LockWithSemaphoreAsync(string title)
      {
         Console.WriteLine($"{title} waiting for lock");

         using (await AsyncSemaphore.WaitAsync())
         {
            Console.WriteLine($"{title} {nameof(LockWithSemaphoreAsync)} started");
            await Task.Delay(500);
            Console.WriteLine($"{title} {nameof(LockWithSemaphoreAsync)} ending");
         }
      }
   }
}