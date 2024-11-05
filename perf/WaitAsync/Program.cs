using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitAsync
{
    class Program
    {        
        static SemaphoreSlim semaphore = new SemaphoreSlim(1);
        const int WaitTimeMs = 1000;        

        static void Main(string[] args)
        {
            Task.Run((Action)Func1);
            Task.Run((Action)Func2);

            //Task.Run((Action)AsyncFunc1);
            //Task.Run((Action)AsyncFunc2);

            Console.ReadKey();            
        }

        static void Func1()
        {
            while (true)
            {
                semaphore.Wait();
                Console.WriteLine("Func1");
                
                semaphore.Release();
                Thread.Sleep(WaitTimeMs);
            }
        }

        static void Func2()
        {
            while (true)
            {
                semaphore.Wait();
                Console.WriteLine("Func2");
                
                semaphore.Release();
                Thread.Sleep(WaitTimeMs);

            }
        }

        static void AsyncFunc1()
        {
            semaphore.WaitAsync().ContinueWith(_ =>
            {
                Console.WriteLine("AsyncFunc1");                
                semaphore.Release();
                Thread.Sleep(WaitTimeMs);
            }).ContinueWith(_ => AsyncFunc1());
        }

        static void AsyncFunc2()
        {
            semaphore.WaitAsync().ContinueWith(_ =>
            {
                Console.WriteLine("AsyncFunc2");                
                semaphore.Release();
                Thread.Sleep(WaitTimeMs);
            }).ContinueWith(_ => AsyncFunc2());
        }
    }
}
