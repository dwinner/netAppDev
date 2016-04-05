/**
 * Обработка ошибок в асинхронных методах
 */

using System;
using System.Threading.Tasks;

namespace ErrorHandling
{
   static class Program
   {
      static void Main()
      {
         HandleOneError();
         DontHandle();
         StartTwoTasks();
         StartTwoTasksParallel();
         ShowAggregatedException();

         Console.ReadLine();
      }

      static async Task ThrowAfter(int ms, string message)
      {
         await Task.Delay(ms);
         throw new Exception(message);
      }

      private static async void HandleOneError()   // Перехват одной ошибки
      {
         try
         {
            await ThrowAfter(2000, "first");
         }
         catch (Exception ex)
         {
            Console.WriteLine("Handled {0}", ex.Message);
         }
      }

      private static void DontHandle() // Ложный перехват
      {
         try
         {
#pragma warning disable 4014
            ThrowAfter(200, "first");  // Note: Исключение не перехватится, потому что метод
#pragma warning restore 4014
            // завершится до возникновения исключения
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }

      private static async void StartTwoTasks() // Последовательный перехват исключения
      {
         try
         {
            await ThrowAfter(2000, "first");
            await ThrowAfter(1000, "second");   // Второго вызова не будет, потому что первый метод
            // сгенерирует исключение
         }
         catch (Exception ex)
         {
            Console.WriteLine("Handled {0}", ex.Message);
         }
      }

      private async static void StartTwoTasksParallel()  // Перехват последнего
      {
         try
         {
            Task t1 = ThrowAfter(2000, "first");
            Task t2 = ThrowAfter(1000, "second");
            await Task.WhenAll(t1, t2);
         }
         catch (Exception ex)
         {
            Console.WriteLine("Handled {0}", ex.Message);   // Отобразится информация только для первой задачи
         }
      }

      private static async void ShowAggregatedException()   // Надежный перехват всех возникших исключений
      {
         Task taskResult = null;
         try
         {
            Task t1 = ThrowAfter(2000, "First");
            Task t2 = ThrowAfter(1000, "Second");
            await (taskResult = Task.WhenAll(t1, t2));
         }
         catch (Exception ex)
         {
            // Просто выводим информацию об исключении для первой задачи
            Console.WriteLine("Handled {0}", ex.Message);
            if (taskResult != null && taskResult.Exception != null)
            {
               foreach (var innerEx in taskResult.Exception.InnerExceptions)
               {
                  Console.WriteLine("inner exception {0} from task {1}",
                     innerEx.Message,
                     innerEx.Source);
               }
            }
         }
      }
   }
}
