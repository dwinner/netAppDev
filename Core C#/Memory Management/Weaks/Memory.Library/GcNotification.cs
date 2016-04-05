using System;
using static System.AppDomain;
using static System.Environment;
using static System.GC;
using static System.Threading.Interlocked;

namespace Memory.Library
{
   /// <summary>
   ///    Уведомления о проходах сборщика мусора
   /// </summary>
   public static class GcNotification
   {
      private static Action<int> _gcDoneAction; // Поле события

      public static event Action<int> GcDone
      {
         add
         {
            // Если зарегистрированные делегаты отсутсвуют, начинаем оповещение
            if (_gcDoneAction == null)
            {
               new GenObject(0);
               new GenObject(2);
            }

            _gcDoneAction += value;
         }
         remove { _gcDoneAction -= value; }
      }

      private sealed class GenObject
      {
         private readonly int _generation;

         public GenObject(int generation)
         {
            _generation = generation;
         }

         ~GenObject()
         {
            // Если объект принадлежит нужному нам поколению (или выше),
            // оповещаем делегат о выполненной сборке мусора
            if (GetGeneration(this) >= _generation)
            {
               var temp = CompareExchange(ref _gcDoneAction, null, null);
               temp?.Invoke(_generation);
            }

            // Продолжаем оповещение, пока остается хоть один зарегистрированный делегат,
            // если домен приложения не выгружен и процесс не завершен
            if (_gcDoneAction != null && !CurrentDomain.IsFinalizingForUnload() && !HasShutdownStarted)
            {
               // Для поколения 0 создаем объект: для поколения 2 воскрешаем объект
               // и позволяем сборщику вызвать метод финализации при следующей сборке
               // мусора для поколения 2
               if (_generation == 0)
               {
                  new GenObject(0);
               }
               else
               {
                  ReRegisterForFinalize(this);
               }
            }
         }
      }
   }
}