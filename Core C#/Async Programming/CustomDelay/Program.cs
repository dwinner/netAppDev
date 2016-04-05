/**
 * Искусственная задержка на указанное время
 */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace _09_CustomDelay
{
   class Program
   {
      static void Main()
      {
         DelayAsync(1000);                // Note: 1) Расточительный вариант
         Thread.CurrentThread.Join(1500);

         DelayElseAsync(1000);            // Note: 2) Искусственный вариант
         Thread.CurrentThread.Join(1500);

         DelayFrameworkAsync(1000);       // Note: 3) Вариант .NET Framework
         Thread.CurrentThread.Join(1500);

         Console.ReadKey(true);
      }

      /// <summary>
      /// Искусственная задержка
      /// </summary>
      /// <remarks>
      ///   Расточительство: мы захватываем поток для того, чтобы заблокировать его
      /// </remarks>
      /// <param name="milliseconds">Задержка в миллисекундах</param>
      private static async void DelayAsync(int milliseconds)
      {
         Console.WriteLine("Doing something..."); // Note: Что-то делать
         await Task.Run(() => Thread.Sleep(milliseconds));
         Console.WriteLine("Doing something after delay..."); // Note: Что-то делать после задержки
      }

      /// <summary>
      /// Искусственная задержка
      /// </summary>
      /// <remarks>
      ///   Здесь используем таймер и задачу-марионетку
      /// </remarks>
      /// <param name="milliseconds">Задержка в миллисекундах</param>
      /// <returns>Задача-марионетка</returns>
      private static Task Delay(int milliseconds)
      {
         TaskCompletionSource<object> taskCompletionSource =
            new TaskCompletionSource<object>();
         Timer timer = new Timer(_ => taskCompletionSource.SetResult(null),
                                 null,
                                 milliseconds,
                                 Timeout.Infinite);
         taskCompletionSource.Task.ContinueWith(delegate { timer.Dispose(); });
         return taskCompletionSource.Task;
      }

      private static async void DelayElseAsync(int milliseconds)
      {
         Console.WriteLine("Doing something");
         await Delay(milliseconds);
         Console.WriteLine("Doing something after delay");
      }

      private static async void DelayFrameworkAsync(int milliseconds)
      {
         Console.WriteLine("Doing something");
         await Task.Delay(milliseconds);
         Console.WriteLine("Doing something after delay");
      }
   }
}
