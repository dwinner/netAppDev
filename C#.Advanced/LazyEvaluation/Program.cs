/*
 * Отложенная инициализация объектов
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LazyEvaluation
{
   public static class Program
   {
      static void Main(string[] args)
      {
         var processes =
            new Lazy<ICollection<string>>(() => Process.GetProcesses().Select(p => p.ProcessName).ToList());

         PrintSystemInfo(processes, true);
         Console.ReadKey();
      }

      static void PrintSystemInfo(Lazy<ICollection<string>> processNames, bool showProcesses)
      {
         Console.WriteLine("MachineName: {0}", Environment.MachineName);
         Console.WriteLine("OS version: {0}", Environment.OSVersion);
         Console.WriteLine("DBG: Is process list created? {0}", processNames.IsValueCreated);
         if (showProcesses)
         {
            Console.WriteLine("Processes:");
            foreach (var processName in processNames.Value)
            {
               Console.WriteLine(processName);
            }
         }
         Console.WriteLine("DBG: Is process list created? {0}", processNames.IsValueCreated);
      }
   }
}
