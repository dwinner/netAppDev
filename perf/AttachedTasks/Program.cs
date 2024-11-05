using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachedTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                Task childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Inside child");
                });

                
            }).ContinueWith(task => Console.WriteLine("Parent done"));

            Task.Factory.StartNew(() =>
            {
                Task childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Inside child");
                });

                childTask.Wait();
            }).ContinueWith(task => Console.WriteLine("Parent done"));

            Task.Factory.StartNew(() =>
            {
                Task childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Inside child");
                }, TaskCreationOptions.AttachedToParent);
                
            }, TaskCreationOptions.DenyChildAttach).ContinueWith(task => Console.WriteLine("Parent done"));
        }
    }
}
