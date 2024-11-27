using System;
using System.Threading.Tasks;

namespace AttachedTasks
{
   internal static class Program
   {
      private static void Main()
      {
         Task.Factory.StartNew(() =>
         {
            var childTask = Task.Factory.StartNew(() => { Console.WriteLine("Inside child"); });
         }).ContinueWith(task => Console.WriteLine("Parent done"));

         Task.Factory.StartNew(() =>
         {
            var childTask = Task.Factory.StartNew(() => { Console.WriteLine("Inside child"); });

            childTask.Wait();
         }).ContinueWith(task => Console.WriteLine("Parent done"));

         Task.Factory.StartNew(() =>
         {
            var childTask = Task.Factory.StartNew(() => { Console.WriteLine("Inside child"); },
               TaskCreationOptions.AttachedToParent);
         }, TaskCreationOptions.DenyChildAttach).ContinueWith(task => Console.WriteLine("Parent done"));
      }
   }
}