using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitAsync
{
   internal static class Program
   {
      private const int WaitTimeMs = 1_000;
      private static readonly SemaphoreSlim _Semaphore = new SemaphoreSlim(1);

      private static void Main(string[] args)
      {
         Task.Run(Func1);
         Task.Run(Func2);

         //Task.Run((Action)AsyncFunc1);
         //Task.Run((Action)AsyncFunc2);

         Console.ReadKey();
      }

      private static void Func1()
      {
         while (true)
         {
            _Semaphore.Wait();
            Console.WriteLine("Func1");

            _Semaphore.Release();
            Thread.Sleep(WaitTimeMs);
         }
      }

      private static void Func2()
      {
         while (true)
         {
            _Semaphore.Wait();
            Console.WriteLine("Func2");

            _Semaphore.Release();
            Thread.Sleep(WaitTimeMs);
         }
      }

      private static void AsyncFunc1()
      {
         _Semaphore.WaitAsync().ContinueWith(_ =>
         {
            Console.WriteLine("AsyncFunc1");
            _Semaphore.Release();
            Thread.Sleep(WaitTimeMs);
         }).ContinueWith(_ => AsyncFunc1());
      }

      private static void AsyncFunc2()
      {
         _Semaphore.WaitAsync().ContinueWith(_ =>
         {
            Console.WriteLine("AsyncFunc2");
            _Semaphore.Release();
            Thread.Sleep(WaitTimeMs);
         }).ContinueWith(_ => AsyncFunc2());
      }
   }
}