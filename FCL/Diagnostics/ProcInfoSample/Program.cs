using System.ComponentModel;
using System.Diagnostics;

namespace ProcInfoSample;

internal static class Program
{
   private static void Main()
   {
      foreach (var process in Process.GetProcesses().Where(pr => pr.ProcessName.StartsWith("L")))
      {
         using (process)
         {
            Console.WriteLine(process.ProcessName);
            Console.WriteLine($"   PID:      {process.Id}");
            Console.WriteLine($"   Memory:   {process.WorkingSet64}");
            Console.WriteLine($"   Threads:  {process.Threads.Count}");
            EnumerateThreads(process);
         }
      }
   }

   private static void EnumerateThreads(Process process)
   {
      try
      {
         foreach (ProcessThread processThread in process.Threads)
         {
            Console.WriteLine(processThread.Id);
            Console.WriteLine($"   State:    {processThread.ThreadState}");
//#if WINDOWS || LINUX
            Console.WriteLine($"   Priority: {processThread.PriorityLevel}");
            Console.WriteLine($"   Started:  {processThread.StartTime}");
//#endif
            Console.WriteLine($"   CPU time: {processThread.TotalProcessorTime}");
         }
      }
      catch (InvalidOperationException ex) // The process may go away while enumerating its threads
      {
         Console.WriteLine($"Exception: {ex.Message}");
      }
      catch (Win32Exception ex) // We may not have access
      {
         Console.WriteLine($"Exception: {ex.Message}");
      }
   }
}