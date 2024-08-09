using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace CancelAsync;

internal static class Program
{
   private static void Main()
   {
      var ob =
         Observable.Create<int>(o =>
            {
               var cancel = new CancellationDisposable(); // internally creates a new CancellationTokenSource
               NewThreadScheduler.Default.Schedule(() =>
                  {
                     var i = 0;
                     for (;;)
                     {
                        Thread.Sleep(200); // here we do the long-lasting background operation
                        if (!cancel.Token.IsCancellationRequested) // check cancel token periodically
                        {
                           o.OnNext(i++);
                        }
                        else
                        {
                           Console.WriteLine("Aborting because cancel event was signaled!");
                           o.OnCompleted();
                           return;
                        }
                     }
                  }
               );

               return cancel;
            }
         );

      var subscription = ob.Subscribe(Console.WriteLine);
      Console.WriteLine("Press any key to cancel");
      Console.ReadKey();
      subscription.Dispose();
      Console.WriteLine("Press any key to quit");
      Console.ReadKey(); // give background thread chance to write the cancel acknowledge message
   }
}