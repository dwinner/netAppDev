using System.Reactive;
using Microsoft.Reactive.Testing;
using Xunit;

namespace RxLibrary.Tests.PlayingWithTestSchduler;

public class CreatColdObservableTests : ReactiveTest
{
   [Fact]
   public void CreatColdObservable_ShortWay()
   {
      var testScheduler = new TestScheduler();
      var coldObservable =
         testScheduler.CreateColdObservable(
            // Inheritting your test class from ReactiveTest opens the following
            // factory methods that make your code much more fluent 
            OnNext(20, 1),
            OnNext(40, 2),
            OnNext(60, 2),
            OnCompleted<int>(900)
         );

      // Creating an observer that captures the emission it recieves
      var testableObserver = testScheduler.CreateObserver<int>();

      // Subscribing the observer, but until TestSchduler is started, emissions 
      // are not be emitted
      coldObservable
         .Subscribe(testableObserver);

      // Starting the TestScheduler means that only now emissions that were configured
      // will be emitted  
      testScheduler.Start();

      // Asserting that every emitted value was recieved by the observer at the 
      // same time it was emitted
      coldObservable.Messages
         .AssertEqual(testableObserver.Messages);

      // Asserting that the observer was subscribed at Scheduler inital time
      coldObservable.Subscriptions.AssertEqual(
         Subscribe(0));
   }

   [Fact]
   public void CreatColdObservable_LongWay()
   {
      var testScheduler = new TestScheduler();

      var coldObservable = testScheduler.CreateColdObservable(
         // This is the long way to configure emissions. see below for a shorter one
         new Recorded<Notification<int>>(20, Notification.CreateOnNext(1)),
         new Recorded<Notification<int>>(40, Notification.CreateOnNext(2)),
         new Recorded<Notification<int>>(60, Notification.CreateOnCompleted<int>())
      );

      // Creating an observer that captures the emission it recieves
      var testableObserver = testScheduler.CreateObserver<int>();


      // Subscribing the observer, but until TestSchduler is started, emissions 
      // are not be emitted
      coldObservable
         .Subscribe(testableObserver);

      // Starting the TestScheduler means that only now emissions that were configured
      // will be emitted  
      testScheduler.Start();

      // Asserting that every emitted value was recieved by the observer at the 
      // same time it was emitted
      coldObservable.Messages
         .AssertEqual(testableObserver.Messages);

      // Asserting that the observer was subscribed at Scheduler inital time
      coldObservable.Subscriptions.AssertEqual(
         Subscribe(0));
   }
}