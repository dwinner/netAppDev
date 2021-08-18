/**
 * Создание собственных комбинаторов параллельности
 */

using System;
using System.Threading.Tasks;

namespace TaskCombinatorsSample
{
   internal static class Program
   {
      private static void Main()
      {
         RunTheTask();
         Console.ReadKey(true);
      }

      private static void RunTheTask()
      {
         Console.WriteLine("Run the task");
         var task = new Task<int>(() =>
         {
            for (var i = 0; i < int.MaxValue - 1; i++)
            {
               Console.WriteLine(i);
            }

            return 1;
         }).WithTimeoutAsync(100);
         task.ContinueWith(async aTask => Console.WriteLine(await aTask.ConfigureAwait(false)));
      }
   }
}