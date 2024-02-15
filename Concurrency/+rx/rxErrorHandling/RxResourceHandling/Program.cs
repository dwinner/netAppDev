using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using RxHelpers;

namespace RxResourceHandling;

internal static class Program
{
   private static void Main()
   {
      TraditionalUsingStatement();
      TheUsingOperator();
      DeterministicDisposal();
      Finally();
      FinallyTestCases();
      WeakRerferecneExample();

      Console.WriteLine("Press <Enter> to exit");
      Console.ReadLine();
   }

   private static void TraditionalUsingStatement()
   {
      using (new DisposableType())
      {
         //Rest of code
      }
   }

   private static void TheUsingOperator()
   {
      Demo.DisplayHeader("The Using operator - gracefully dispose a resource when the observable terminates");

      var logFilePath = Path.Combine(
         Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(),
         "example.log");
      var sensorData =
         Observable.Range(1, 3)
            .Select(x => new SensorData(x));

      var sensorDataWithLogging =
         Observable.Using(() => new StreamWriter(logFilePath),
            writer => sensorData.Do(x => writer.WriteLine(x.Data)));

      sensorDataWithLogging.SubscribeConsole("sensor");
   }

   private static void DeterministicDisposal()
   {
      Demo.DisplayHeader(
         "The Using operator will make sure that the resource is disposed no matter what caused the observable to stop");

      var subject = new Subject<int>();
      var lSbj = subject;
      var observable =
         Observable.Using(() => Disposable.Create(() => { Console.WriteLine("DISPOSED"); }),
            _ => lSbj);

      Console.WriteLine();
      Console.WriteLine("Disposed when completed");
      observable.SubscribeConsole();
      subject.OnCompleted();

      Console.WriteLine();
      Console.WriteLine("Disposed when error occurs");
      subject = new Subject<int>();
      observable.SubscribeConsole();
      subject.OnError(new Exception("error"));

      Console.WriteLine();
      Console.WriteLine("Disposed when subscription disposed");
      subject = new Subject<int>();
      var subscription =
         observable.SubscribeConsole();
      subscription.Dispose();
   }

   private static void WeakRerferecneExample()
   {
      Demo.DisplayHeader("WeakReference");

      var obj = new object();
      var weak = new WeakReference(obj);

      GC.Collect();
      Console.WriteLine("IsAlive: {0} obj!=null is {1}", weak.IsAlive, obj != null);

      obj = null;
      GC.Collect();
      Console.WriteLine("IsAlive: {0}", weak.IsAlive);
   }

   private static void Finally()
   {
      var progress =
         Observable.Range(1, 3);

      progress
         .Finally(() =>
         { /*close the window*/
         })
         .Subscribe(x =>
         { /*Update the UI*/
         });
   }

   private static void FinallyTestCases()
   {
      Demo.DisplayHeader(
         "The Finally operator - runs an action when the observable terminates, wither gracefully or due to an error");

      Console.WriteLine();
      Console.WriteLine("Successful complete");
      Observable.Empty<int>()
         .Finally(() => Console.WriteLine("Finally Code"))
         .SubscribeConsole();

      Console.WriteLine();
      Console.WriteLine("Error termination");
      Observable.Throw<Exception>(new Exception("error"))
         .Finally(() => Console.WriteLine("Finally Code"))
         .SubscribeConsole();

      Console.WriteLine();
      Console.WriteLine("Unsubscribing");
      var subject = new Subject<int>();
      var subscription =
         subject.AsObservable()
            .Finally(() => Console.WriteLine("Finally Code"))
            .SubscribeConsole();
      subscription.Dispose();
   }

   private class DisposableType : IDisposable
   {
      public void Dispose()
      { /*Freeing the resource*/
      }
   }
}