using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancellation
{
   internal static class Program
   {
      private static void Main()
      {
         var tokenSource = new CancellationTokenSource();
         var token = tokenSource.Token;

         var task = Task.Run(() =>
         {
            while (true)
            {
               // do some work...
               if (token.IsCancellationRequested)
               {
                  Console.WriteLine("Cancellation requested");
                  return;
               }

               Thread.Sleep(100);
            }
         }, token);

         Console.WriteLine("Press any key to exit");

         Console.ReadKey();

         tokenSource.Cancel();

         task.Wait();

         Console.WriteLine("Task completed");
      }
   }
}