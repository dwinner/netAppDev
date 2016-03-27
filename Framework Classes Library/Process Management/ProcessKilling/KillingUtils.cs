using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace Killing
{
   /// <summary>
   ///     Утилиты для принудительного завершения процессов
   /// </summary>
   public static class KillingUtils
   {
      /// <summary>
      ///     Завершение дочерних процессов
      /// </summary>
      /// <param name="parentProcessId">Идентификатор процесса</param>
      /// <param name="killParent">true, если нужно уничтожить родительский процесс, false - если нет</param>
      public static void KillProcessesSpawnedBy(UInt32 parentProcessId, bool killParent = true)
      {
         // NOTE: Идентификаторы процессов могут быть переиспользованы ОС Windows!
         bool hasChildren;
         var childProcessIds = GetChildProcessIds(parentProcessId, out hasChildren);
         if (hasChildren)
         {
            foreach (var childProcessId in childProcessIds)
            {
               KillProcessesSpawnedBy(childProcessId, killParent);
               Kill(childProcessId);
            }
         }

         if (killParent)
         {
            Kill(parentProcessId);
         }
      }

      /// <summary>
      ///     Завершение дочерних процессов
      /// </summary>
      /// <param name="parentProcess">Родительский процесс</param>
      /// <param name="killParent">true, если нужно уничтожить родительский процесс, false - если нет</param>
      public static void KillProcessesSpawnedBy(Process parentProcess, bool killParent = true)
      {
         bool hasChildren;
         var childProcesses = GetChildProcesses((uint)parentProcess.Id, out hasChildren);
         if (hasChildren)
         {
            foreach (var childProcess in childProcesses)
            {
               KillProcessesSpawnedBy(childProcess, killParent);
               Unmanaged.TerminateProcess(childProcess.Handle, 0);
            }
         }

         if (killParent)
         {
            Unmanaged.TerminateProcess(parentProcess.Handle, 0);
         }
      }

      /// <summary>
      ///     Получает набор идентификаторов для дочерних процессов
      /// </summary>
      /// <param name="parentProcessId">Идентификатор родительского процесса</param>
      /// <param name="hasChildren">true, если дочерние процессы есть, false - в противном случае</param>
      /// <returns>Набор идентификаторов для дочерних процессов</returns>
      private static IEnumerable<uint> GetChildProcessIds(uint parentProcessId, out bool hasChildren)
      {
         var searcher =
             new ManagementObjectSearcher(string.Format("SELECT * FROM Win32_Process WHERE ParentProcessId={0}",
                 parentProcessId));
         var managementObjects = searcher.Get();
         if (managementObjects.Count > 0)
         {
            hasChildren = true;
            return
                managementObjects.Cast<ManagementBaseObject>()
                    .Select(item => (uint)item["ProcessId"])
                    .Where(childId => (int)childId != Process.GetCurrentProcess().Id);
         }

         hasChildren = false;
         return Enumerable.Empty<uint>();
      }

      /// <summary>
      ///     Получение дочерних процессов
      /// </summary>
      /// <param name="parentProcessId">Идентификатор родительского процесса</param>
      /// <param name="hasChildren">true, если дочерние процессы есть, false - в противном случае</param>
      /// <returns>Дочерние процессы</returns>
      private static IEnumerable<Process> GetChildProcesses(uint parentProcessId, out bool hasChildren)
      {
         var childProcessIds = GetChildProcessIds(parentProcessId, out hasChildren);
         return hasChildren
             ? childProcessIds.Select(childId => Process.GetProcessById((int)childId))
             : Enumerable.Empty<Process>();
      }

      /// <summary>
      ///     Управляемое уничтожение процесса
      /// </summary>
      /// <param name="processId">Идентификатор процесса</param>
      private static void Kill(uint processId)
      {
         try
         {
            var process = Process.GetProcessById((int)processId);
            process.Kill();
         }
         catch (Exception)
         {
            // ignored
         }
      }
   }
}