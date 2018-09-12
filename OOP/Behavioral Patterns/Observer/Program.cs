/**
 * Классическая реализация шаблона публикации-подписки
 */

using System;
using System.Threading;

namespace Observer
{
   internal static class Program
   {
      private const int Speed = 100;

      private static void Main()
      {
         var simulator = new Simulator();
         var subject = new SubjectImpl();
         IObserver<string> firstObserver = new ObserverImpl(subject, "Center", "\t\t");
         IObserver<string> secondObserver = new ObserverImpl(subject, "Right", "\t\t\t\t");
         subject.Add(firstObserver, secondObserver);

         ThreadPool.QueueUserWorkItem(state =>
         {
            foreach (var s in simulator)
            {
               Console.WriteLine("Subject: {0}", s);
               subject.SubjectState = s; // Raise notifications for observers
               Thread.Sleep(Speed);
            }
         });

         Console.ReadLine();
      }
   }
}