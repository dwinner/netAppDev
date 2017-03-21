using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;

namespace ProgramVerificationSystems.SelfTester.Model.Utils
{
   /// <summary>
   ///    Утилиты для принудительного завершения процессов
   /// </summary>
   public static class ProcessUtils
   {
      private const string Win32ProcessesWmiQuery = "SELECT * FROM Win32_Process WHERE ParentProcessId={0}";

      /// <summary>
      ///    Завершение дочерних процессов
      /// </summary>
      /// <param name="parentProcess">Родительский процесс</param>
      /// <param name="killParent">true, если нужно уничтожить родительский процесс, false - если нет</param>
      public static void MandatoryKill(Process parentProcess, bool killParent = true)
      {
         try
         {
            bool hasChildren;
            var childProcesses = GetChildProcesses((uint)parentProcess.Id, out hasChildren);
            if (hasChildren)
            {
               foreach (var childProcess in childProcesses)
               {
                  MandatoryKill(childProcess, killParent);
                  TerminateParent(childProcess);
               }
            }

            if (killParent)
               TerminateParent(parentProcess);
         }
         catch (InvalidOperationException)
         {
         }
         catch (ArgumentException)
         {
         }
         catch (Exception)
         {
            // ignored
         }
      }

      public static IEnumerable<uint> GetChildProcessIds(uint parentProcId, string processName)
      {
         var searcher = new ManagementObjectSearcher(string.Format(Win32ProcessesWmiQuery, parentProcId));
         var managementObjects = searcher.Get().Cast<ManagementBaseObject>();
         return from managementObject in managementObjects
                let childProcName = (string)managementObject["Name"]
                where childProcName == processName
                select Convert.ToUInt32(managementObject["Handle"]);
      }

      public static bool ExistsChildProcess(uint parentProcId, string processName)
      {
         var searcher = new ManagementObjectSearcher(string.Format(Win32ProcessesWmiQuery, parentProcId));
         var managementObjects = searcher.Get().Cast<ManagementBaseObject>();
         return
            managementObjects.Select(managementObject => (string)managementObject["Name"])
               .Any(childProcName => childProcName == processName);
      }

      /// <summary>
      ///    Получает набор идентификаторов для дочерних процессов
      /// </summary>
      /// <param name="parentProcessId">Идентификатор родительского процесса</param>
      /// <param name="hasChildren">true, если дочерние процессы есть, false - в противном случае</param>
      /// <returns>Набор идентификаторов для дочерних процессов</returns>
      private static IEnumerable<uint> GetChildProcessIds(uint parentProcessId, out bool hasChildren)
      {
         var searcher = new ManagementObjectSearcher(string.Format(Win32ProcessesWmiQuery, parentProcessId));
         var managementObjects = searcher.Get();
         if (managementObjects.Count > 0)
         {
            hasChildren = true;
            return
               managementObjects
                  .Cast<ManagementBaseObject>()
                  .Select(item => (uint)item["ProcessId"])
                  .Where(childId => (int)childId != Process.GetCurrentProcess().Id);
         }

         hasChildren = false;
         return Enumerable.Empty<uint>();
      }

      /// <summary>
      ///    Получение дочерних процессов
      /// </summary>
      /// <param name="parentProcessId">Идентификатор родительского процесса</param>
      /// <param name="hasChildren">true, если дочерние процессы есть, false - в противном случае</param>
      /// <returns>Дочерние процессы</returns>
      private static IEnumerable<Process> GetChildProcesses(uint parentProcessId, out bool hasChildren)
      {
         var childProcessIds = GetChildProcessIds(parentProcessId, out hasChildren).ToList();
         if (!hasChildren)
            return Enumerable.Empty<Process>();

         var activeProcesses = new List<Process>();
         foreach (var childProcessId in childProcessIds)
         {
            try
            {
               activeProcesses.Add(Process.GetProcessById((int)childProcessId));
            }
            catch (InvalidOperationException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
               // ignored
            }
         }

         return activeProcesses;
      }

      private static void TerminateParent(Process parentProcess)
      {
         string errorMessage;
         Unmanaged.TryTerminateProcess(parentProcess, out errorMessage);
      }

      /// <summary>
      ///    Получение командной строки для процесса через WMI
      /// </summary>
      /// <param name="process">Процесс</param>
      /// <returns>Командная строка процесса</returns>
      /// <remarks>Предполагается, что процесс еще существует</remarks>
      public static string GetCommandLine(this Process process)
      {
         var commandLine = new StringBuilder(process.MainModule.FileName);

         commandLine.Append(" ");
         using (
            var searcher =
               new ManagementObjectSearcher(string.Format(
                  "SELECT CommandLine FROM Win32_Process WHERE ProcessId = {0}", process.Id)))
         {
            foreach (var @object in searcher.Get())
            {
               commandLine.Append(@object["CommandLine"]);
               commandLine.Append(" ");
            }
         }

         return commandLine.ToString();
      }
   }
}