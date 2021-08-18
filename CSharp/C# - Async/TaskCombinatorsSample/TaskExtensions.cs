using System;
using System.Threading.Tasks;

namespace TaskCombinatorsSample
{
   public static class TaskExtensions
   {
      /// <summary>
      ///    Добавление к произвольной задаче таймаута
      /// </summary>
      /// <remarks>Этот метод не блокирует вызывающий поток</remarks>
      /// <typeparam name="T">Параметр типа для расширения класса Task</typeparam>
      /// <param name="task">Объект расширения класса Task</param>
      /// <param name="milliseconds">Значение таймаута</param>
      /// <returns>Значение результата выполнения задачи "в будущем"</returns>
      /// <exception cref="TimeoutException">Если задаче не удалось завершиться за отведенный таймаут</exception>
      public static async Task<T> WithTimeoutAsync<T>(this Task<T> task, int milliseconds)
      {
         var delayTask = Task.Delay(milliseconds);
         var firstToFinish = await Task.WhenAny(task, delayTask)
            .ConfigureAwait(false);

         if (firstToFinish == delayTask)
         {
            // Note: Первой завершилась задача задержки. Разберемся с исключениями
            await task.ContinueWith(aTask =>
            {
               if (aTask.Exception != null)
               {
                  Console.WriteLine("Task exception: {0}", aTask.Exception.Message);
               }
            }).ConfigureAwait(false);
            throw new TimeoutException("Task timeout");
         }

         // Note: Если мы дошли до этого места, исходная задача уже завершилась
         // Note: Здесь await нужен только для удобства. Т.к. задача завершена,
         // Note: метод продолжится синхронно
         return /*task.Result*/ await task.ConfigureAwait(false);
      }

      //private static void HandleException<T>(Task<T> aTask)
      //{
      //   if (aTask.Exception != null)
      //   {
      //      Console.WriteLine("Task exception: {0}", aTask.Exception.Message); // ToDo: Логируем исключение
      //   }
      //}
   }
}