using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskException
{
    class Program
    {
        static void Main(string[] args)
        {
            var failingTask = Task<int>.Factory.StartNew(() =>
            {
                int x = 42;
                int y = 0;
                return x / y;
            });
            failingTask.Wait();
            int result = failingTask.Result;

            Task<int>.Factory.StartNew(() =>
            {
                int x = 42;
                int y = 0;
                return x / y;
            }).ContinueWith(task =>
            {
                int val = task.Result;
            });

            Task<int>.Factory.StartNew(() =>
            {
                int x = 42;
                int y = 0;
                return x / y;
            }).ContinueWith(task =>
            {
                try
                {
                    // safely handle result
                    int val = task.Result;
                }catch(AggregateException ex)
                {
                    LogException(ex);
                }                
            });

            Task<int>.Factory.StartNew(() =>
            {
                int x = 42;
                int y = 0;
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
                    int val = task.Result;
                }
            });

            ThreadPool.QueueUserWorkItem(new WaitCallback(MyFunc), "my data");
        }

        private static void MyFunc(Object obj)
        {
            var data = obj as string;
            // do work
        }

        static void LogException(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
