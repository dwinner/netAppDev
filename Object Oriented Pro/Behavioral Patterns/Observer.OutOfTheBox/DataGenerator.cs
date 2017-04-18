using System;
using System.Collections.Generic;

namespace Observer.OutOfTheBox
{
   internal class DataGenerator : IObservable<int>
   {
      private static readonly Random _Rand = new Random();
      private readonly List<IObserver<int>> _observers = new List<IObserver<int>>();
      private int _lastPrime = -1;

      public IDisposable Subscribe(IObserver<int> observer)
      {
         _observers.Add(observer);
         observer.OnNext(_lastPrime);
         
         return observer as IDisposable;
      }

      private void NotifyData(int n)
         => _observers.ForEach(observer => observer.OnNext(n));

      private void NotifyComplete()
         => _observers.ForEach(observer => observer.OnCompleted());

      public void Run()
      {
         for (var i = 0; i < 100; ++i)
         {
            var n = _Rand.Next(1, int.MaxValue);
            if (IsPrime(n))
            {
               _lastPrime = n;
               NotifyData(n);
            }
         }

         NotifyComplete();
      }

      private static bool IsPrime(int number)
      {
         if (number % 2 == 0)
            return number == 2;

         var max = (int) Math.Sqrt(number);
         for (long i = 3; i <= max; i += 2)
            if (number % i == 0)
               return false;

         return true;
      }
   }
}