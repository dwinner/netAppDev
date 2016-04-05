/**
 * Базовые концепции асинхронного программирования
 */

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Foundations
{
   static class Program
   {
      static void Main()
      {
         // Note: Явная установка контекста синхронизации
         //var ctx = new DispatcherSynchronizationContext();
         //SynchronizationContext.SetSynchronizationContext(ctx);

         Console.WriteLine(Greeting("Denny"));
         CallerWithAsync();
         CallerWithAsync2();
         CallerWithContinuationTask();
         CallerWithAwaiter();
         MultipleAsyncMethods();
         MultipleAsyncMethodsWithCombinators1();
         MultipleAsyncMethodsWithCombinators2();
         ConvertingAsyncPattern();

         Console.ReadLine();
      }

      private static string Greeting(string name)  // Синхронный метод с задержкой выполнения
      {
         Console.WriteLine("Running greeting in thread {0} and task {1}",
            Thread.CurrentThread.ManagedThreadId,
            Task.CurrentId);
         Thread.Sleep(3000);
         return string.Format("Hello, {0}", name);
      }

      private static Task<string> GreetingAsync(string name)   // Обертка синхронного метода для возврата задачи
      {
         return Task.Run(() => Greeting(name));
      }

      private static async void CallerWithAsync()  // Вызов асинхронного метода
      {
         Console.WriteLine("Started CallerWithAsync in thread {0} and task {1}",
            Thread.CurrentThread.ManagedThreadId,
            Task.CurrentId);
         string result = await GreetingAsync("Stephanie");
         Console.WriteLine(result);
         Console.WriteLine("Finished GreetingAsync in thread {0} and task {1}",
            Thread.CurrentThread.ManagedThreadId,
            Task.CurrentId);
      }

      private static async void CallerWithAsync2() // Вызов асинхронного метода 2
      {
         Console.WriteLine("Started CallerWithAsync in thread {0} and task {1}",
            Thread.CurrentThread.ManagedThreadId,
            Task.CurrentId);
         Console.WriteLine(await GreetingAsync("Stephanie"));
         Console.WriteLine("Finished GreetingAsync in thread {0} and task {1}",
            Thread.CurrentThread.ManagedThreadId,
            Task.CurrentId);
      }

      private static void CallerWithContinuationTask()   // Вызов метода асинхронным образом с задачей продолжения
      {
         Console.WriteLine("Started CallerWithContinuationTask in thread {0} and task {1}",
            Thread.CurrentThread.ManagedThreadId,
            Task.CurrentId);
         Task<string> aTask = GreetingAsync("Stephanie");
         aTask.ConfigureAwait(false);  // Note: Не восстанавливать тот же контекст синхронизации в задаче продолжения
         aTask.ContinueWith(task =>
         {
            string result = task.Result;
            Console.WriteLine(result);
            Console.WriteLine("finished CallerWithContinuationTask in thread {0} and task {1}",
               Thread.CurrentThread.ManagedThreadId,
               Task.CurrentId);
         });
      }

      private static void CallerWithAwaiter()   // Объекты ожидания
      {
         Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
         TaskAwaiter<string> awaiter = GreetingAsync("Matthias").GetAwaiter();
         awaiter.OnCompleted(() => Console.WriteLine("Await is finished"));
         string result = awaiter.GetResult();
         Console.WriteLine(result);
         Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
      }

      private async static void MultipleAsyncMethods()   // Множественный вызов асинхронных методов
      {
         string s1 = await GreetingAsync("Stephanie");
         string s2 = await GreetingAsync("Matthias");
         Console.WriteLine("Finished both methods.\n Result 1: {0}\n Result 2: {1}", s1, s2);
      }

      private async static void MultipleAsyncMethodsWithCombinators1()  // Использование комбинаторов
      {
         Task<string> t1 = GreetingAsync("Stephanie");
         Task<string> t2 = GreetingAsync("Matthias");
         await Task.WhenAll(t1, t2);
         Console.WriteLine("Finished both methods.\n Result 1: {0}\n Result 2: {1}", t1.Result, t2.Result);
      }

      private async static void MultipleAsyncMethodsWithCombinators2()  // Возвращение результатов из комбинаторов
      {
         Task<string> t1 = GreetingAsync("Stephanie");
         Task<string> t2 = GreetingAsync("Matthias");
         string[] results = await Task.WhenAll(t1, t2);
         Console.WriteLine("Finished both methods.\n Result 1: {0}\n Result 2: {1}", results[0], results[1]);
      }

      #region Преобразование асинхронного шаблона в TAP

      private static readonly Func<string, string> GreetingInvoker = Greeting;

      static IAsyncResult BeginGreeting(string name, AsyncCallback callback, object state)
      {
         return GreetingInvoker.BeginInvoke(name, callback, state);
      }

      static string EndGreeting(IAsyncResult asyncResult)
      {
         return GreetingInvoker.EndInvoke(asyncResult);
      }

      private static async void ConvertingAsyncPattern()
      {
         string result = await Task<string>.Factory.FromAsync(BeginGreeting, EndGreeting, "Angela", null);
         Console.WriteLine(result);
      }

      #endregion

      #region Общие обертки синхронных методов для возврата задачи

      //private static Task<string> GreetingAsync(string name, Func<string, string> wrapperFunc)
      //{
      //   return Task.Run(() => wrapperFunc(name));
      //}

      //private static Task<TOutput> GreetingAsync<TInput, TOutput>(TInput value, Func<TInput, TOutput> wrapperFunc)
      //{
      //   return Task.Run(() => wrapperFunc(value));
      //}

      #endregion

   }
}
