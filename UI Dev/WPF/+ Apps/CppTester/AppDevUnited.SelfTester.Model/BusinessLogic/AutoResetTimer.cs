using System;
using System.Threading;

namespace AppDevUnited.SelfTester.Model.BusinessLogic
{
   /// <summary>
   ///     Автоматически сбрасываемый таймер CLR-ThreadPool'а
   /// </summary>
   public sealed class AutoResetTimer : IDisposable
   {
      private const int DefaultCallInterval = 0;

      private static Timer _computeTimer;
      private readonly Action _backgroundAction;
      private readonly int _callInterval;

      private AutoResetTimer(Action backgroundAction, int callInterval = DefaultCallInterval)
      {
         _backgroundAction = backgroundAction;
         _callInterval = callInterval;
      }

      public void Dispose()
      {
         _computeTimer.Dispose();
      }

      /// <summary>
      ///     Factory-метод создания AutoReset-таймера
      /// </summary>
      /// <param name="backgroundAction">Асинхронная периодическая операция</param>
      /// <param name="callInterval">Интервал в мс. между вызовами</param>
      /// <returns>Объект AutoReset-таймера</returns>
      public static AutoResetTimer Create(Action backgroundAction, int callInterval)
      {
         return new AutoResetTimer(backgroundAction, callInterval);
      }

      /// <summary>
      ///     Запуск асинхронной периодической операции в пуле потоков
      /// </summary>
      /// <param name="startAction">Начальная операция</param>
      /// <exception cref="Exception">A delegate callback throws an exception.</exception>
      public AutoResetTimer Shedule(Action startAction = null)
      {
         _computeTimer = new Timer(ComputeBoundOperation, null, 0, Timeout.Infinite);
         if (startAction != null)
         {
            startAction();
         }

         return this;
      }

      private void ComputeBoundOperation(object state)
      {
         _backgroundAction();
         try
         {
            _computeTimer.Change(_callInterval, Timeout.Infinite);
         }
         catch (ObjectDisposedException)
         {
            // Есть вероятность, что таймер покинул область видимости
         }
      }
   }
}