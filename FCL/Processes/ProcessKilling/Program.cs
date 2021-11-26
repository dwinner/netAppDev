/**
 * Принудительное уничтожение процессов
 */

using System;
using System.Diagnostics;

namespace Killing
{
   internal static class Program
   {
      private static void Main()
      {
         // Завершение управляемым кодом
         Console.Write("Enter the processId for managed termination: ");
         var procIdStr = Console.ReadLine();
         int procId;
         if (!string.IsNullOrEmpty(procIdStr) && int.TryParse(procIdStr.Trim(), out procId))
         {
            KillingUtils.KillProcessesSpawnedBy((uint)procId);
            Console.WriteLine("Managed killing successfully");
         }

         // Завершение неуправляемым кодом
         Console.Write("Enter the processId for unmanaged termination: ");
         procIdStr = Console.ReadLine();
         if (!string.IsNullOrEmpty(procIdStr) && int.TryParse(procIdStr.Trim(), out procId))
         {
            KillingUtils.KillProcessesSpawnedBy(Process.GetProcessById(procId));
            Console.WriteLine("Unmanaged killing successfully");
         }
      }
   }
}