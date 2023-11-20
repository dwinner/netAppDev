using System;
using System.Threading;
using System.Threading.Tasks;
using Helpers;

namespace SimpleAsyncProgram
{
   internal class AsyncMethodIsNotAlwaysAsync
   {
      public static async Task AsyncMethodCaller()
      {
         Console.WriteLine();
         Demo.DisplayHeader("Async methods are not always async");

         var isSame = await MyAsyncMethod(Thread.CurrentThread.ManagedThreadId);
         Console.WriteLine("Caller thread is the same as executing thread: {0}", isSame); //this will print 'true'
      }

      private static async Task<bool> MyAsyncMethod(int callingThreadId) =>
         Thread.CurrentThread.ManagedThreadId == callingThreadId;
   }
}