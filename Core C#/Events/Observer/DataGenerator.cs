using System;
using System.Collections.Generic;

namespace Inbox.Observer
{
   /// <summary>
   ///    Класс-поставщик уведомлений
   /// </summary>
   public class DataGenerator : IObservable<int>
   {
      private static readonly Random Random = new Random();
      private readonly IList<IObserver<int>> _observers = new List<IObserver<int>>();
      private int _lastPrime = -1;

      public IDisposable Subscribe(IObserver<int> observer)
      {
         _observers.Add(observer);
         observer.OnNext(_lastPrime);
         return new ObserverAdapter<int>(observer);
      }

      public void Run() // Генератор произвольных данных
      {
         for (var i = 0; i < 100; i++)
         {
            var n = Random.Next(1, int.MaxValue);
            if (IsPrime(n))
            {
               _lastPrime = n;
               NotifyData(n);
            }
         }
         NotifyComplete();
      }

      private void NotifyData(int n) // Уведомляет всех подписчиков о появлении новых данных
      {
         foreach (var observer in _observers)
         {
            observer.OnNext(n);
         }
      }

      private void NotifyComplete() // Уведомляет всех подписчиков о прекращении поступления новых данных
      {
         foreach (var observer in _observers)
         {
            observer.OnCompleted();
         }
      }

      private static bool IsPrime(int number)
      {
         if (number%2 == 0)
         {
            return number == 2;
         }

         var max = (int) Math.Sqrt(number);
         for (var i = 3; i <= max; i += 2)
         {
            if ((number%i) == 0)
            {
               return false;
            }
         }

         return true;
      }

      #region Адаптер для наблюдателя

      private sealed class ObserverAdapter<T> : IDisposable
      {
         // ReSharper disable once NotAccessedField.Local
         private readonly IObserver<T> _observer;

         public ObserverAdapter(IObserver<T> observer)
         {
            _observer = observer;
         }

         public void Dispose()
         {
         }
      }

      #endregion
   }
}