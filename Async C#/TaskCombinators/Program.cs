/**
 * Создание собственных комбинаторов параллельности
 */

using System;
using System.Threading.Tasks;

namespace _10_TaskCombinators
{
   class Program
   {
      static void Main()
      {
         RunTheTask();

         Console.ReadKey(true);
      }

      private static void RunTheTask()
      {
         Console.WriteLine("Run the task");
         Task<int> task = new Task<int>(() =>
         {
            for (int i = 0; i < int.MaxValue - 1; i++)
            {
               Console.WriteLine(i);
            }
            return 1;
         }).WithTimeout(100);
         task.ContinueWith(async aTask => Console.WriteLine(await aTask));
      }
   }
}
