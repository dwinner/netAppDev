﻿using System.Reactive.Linq;
using RxHelpers;

namespace RxSynchronize;

internal static class Program
{
   private static void Main()
   {
      OneObservableSynchronization();
      MultipleObservableSynchronization();

      Console.ReadLine();
   }

   private static void OneObservableSynchronization()
   {
      Demo.DisplayHeader(
         "The Synchrnoize operator - synchronizes the notifications so they will be received in a seriazlied way");

      var messenger = new Messenger();
      var messages =
         Observable.FromEventPattern<string>(
            h => messenger.MessageReceived += h,
            h => messenger.MessageReceived -= h);

      messages
         .Select(evt => evt.EventArgs)
         .Synchronize()
         .Subscribe(msg =>
         {
            Console.WriteLine("Message {0} arrived", msg);
            Thread.Sleep(1000);
            Console.WriteLine("Message {0} exit", msg);
         });

      for (var i = 0; i < 3; i++)
      {
         var msg = "msg" + i;
         ThreadPool.QueueUserWorkItem(_ => { messenger.Notify(msg); });
      }

      //waiting for all the other threads to complete before proceeding
      Thread.Sleep(2000);
   }

   private static void MultipleObservableSynchronization()
   {
      Demo.DisplayHeader(
         "The Synchrnoize operator - can synchronizes the notifications from multiple observables by passing the gate object");

      var messenger = new Messenger();
      var messages =
         Observable.FromEventPattern<string>(
            h => messenger.MessageReceived += h,
            h => messenger.MessageReceived -= h);

      var friendRequests =
         Observable.FromEventPattern<FriendRequest>(
            h => messenger.FriendRequestReceived += h,
            h => messenger.FriendRequestReceived -= h);

      var gate = new object();

      messages
         .Select(evt => evt.EventArgs)
         .Synchronize(gate)
         .Subscribe(msg =>
         {
            Console.WriteLine("Message {0} arrived", msg);
            Thread.Sleep(1000);
            Console.WriteLine("Message {0} exit", msg);
         });


      friendRequests
         .Select(evt => evt.EventArgs)
         .Synchronize(gate)
         .Subscribe(request =>
         {
            Console.WriteLine("user {0} sent request", request.UserId);
            Thread.Sleep(1000);
            Console.WriteLine("user {0} approved", request.UserId);
         });
      for (var i = 0; i < 3; i++)
      {
         var msg = "msg" + i;
         var userId = "user" + i;
         ThreadPool.QueueUserWorkItem(_ => { messenger.Notify(msg); });

         ThreadPool.QueueUserWorkItem(_ => { messenger.Notify(new FriendRequest { UserId = userId }); });
      }

      //waiting for all the other threads to complete before proceeding
      Thread.Sleep(3000);
   }
}