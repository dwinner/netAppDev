﻿using System.Reactive;
using System.Reactive.Linq;
using RxHelpers;

namespace RxCreatingObservers;

internal static class Program
{
   private static void Main()
   {
      CreatingWithFullSubscribeOverload();
      CreatingWithSimplestSubscribeOverload();
      CreatingWithSimplestSubscribeOverload();
      CreatingWithSimplestSubscribeOverloadOnAsyncSource();
      SubscribeWithCancellationInsteadOfDisposable();
      ObserverCreate();

      Console.ReadLine();
   }

   private static void ObserverCreate()
   {
      Demo.DisplayHeader("using Observer.Create() to share the observer with two observables");

      var observer = Observer.Create<string>(Console.WriteLine);

      var subscription1 =
         Observable.Interval(TimeSpan.FromSeconds(1))
            .Select(x => "X" + x)
            .Subscribe(observer);

      var subscription2 =
         Observable.Interval(TimeSpan.FromSeconds(2))
            .Select(x => "YY" + x)
            .Subscribe(observer);

      Console.WriteLine("Unsubscribing in 5 seconds");
      Thread.Sleep(5000);
      subscription1.Dispose();
      subscription2.Dispose();
   }

   private static void SubscribeWithCancellationInsteadOfDisposable()
   {
      Demo.DisplayHeader("Subscribing with CancellationToken to replace the IDisposable");

      var cts = new CancellationTokenSource();
      cts.Token.Register(() => Console.WriteLine("Subscription cancelled"));

      Observable.Interval(TimeSpan.FromSeconds(1))
         .Subscribe(Console.WriteLine, cts.Token);

      Console.WriteLine("Cancelling in five seconds");
      cts.CancelAfter(TimeSpan.FromSeconds(5));

      cts.Token.WaitHandle.WaitOne();
   }

   private static void CreatingWithSimplestSubscribeOverloadOnAsyncSource()
   {
      Demo.DisplayHeader(
         "Creating observer with the simplest Subscribe(...) overload can hide bugs (Async version)");

      Observable.Range(1, 5)
         .Select(x =>
            Task.Run(() =>
               x / (x - 3))) //making the calculation asynchronous - so the when x=3 an exception will occur on another thread
         .Concat() //keeping the results in the same order as the numbers that created them
         .Subscribe(x => Console.WriteLine("{0}", x));
   }

   private static void CreatingWithSimplestSubscribeOverload()
   {
      Demo.DisplayHeader("Creating observer with the simplest Subscribe(...) overload can hide bugs");

      try
      {
         Observable.Range(1, 5)
            .Select(x => x / (x - 3))
            .Subscribe(x => Console.WriteLine("{0}", x));
      }
      catch (Exception)
      {
         Console.WriteLine("we got exception");
      }

      Console.WriteLine("We solve it even by simply adding an empty OnError function");
      Observable.Range(1, 5)
         .Select(x => x / (x - 3))
         .Subscribe(x => Console.WriteLine("{0}", x),
            ex =>
            { /* do something with the exception */
            });
   }

   private static void CreatingWithFullSubscribeOverload()
   {
      Demo.DisplayHeader("Creating observer with the full Subscribe(...) overload");
      Observable.Range(1, 5)
         .Subscribe(
            x => Console.WriteLine("OnNext({0})", x),
            ex => Console.WriteLine("OnError: {0}", ex.Message),
            () => Console.WriteLine("OnCompleted")
         );
   }
}