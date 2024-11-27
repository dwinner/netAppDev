using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskException
{
   internal static class Program
   {
      private static void Main()
      {
         var failingTask = Task<int>.Factory.StartNew(() =>
         {
            var x = 42;
            var y = 0;
            return x / y;
         });
         failingTask.Wait();
         var result = failingTask.Result;

         Task<int>.Factory.StartNew(() =>
         {
            var x = 42;
            var y = 0;
            return x / y;
         }).ContinueWith(task =>
         {
            var val = task.Result;
         });

         Task<int>.Factory.StartNew(() =>
         {
            var x = 42;
            var y = 0;
            return x / y;
         }).ContinueWith(task =>
         {
            try
            {
               // safely handle result
               var val = task.Result;
            }
            catch (AggregateException ex)
            {
               LogException(ex);
            }
         });

         Task<int>.Factory.StartNew(() =>
         {
            var x = 42;
            var y = 0;
            return x / y;
         }).ContinueWith(task =>
         {
            if (task.IsFaulted)
            {
               LogException(task.Exception);
            }
            else
            {
               // safely handle result
               var val = task.Result;
            }
         });

         ThreadPool.QueueUserWorkItem(MyFunc, "my data");
      }

      private static void MyFunc(object obj)
      {
         var data = obj as string;
         // do work
      }

      private static void LogException(Exception ex)
      {
         Console.WriteLine(ex);
      }
   }
}