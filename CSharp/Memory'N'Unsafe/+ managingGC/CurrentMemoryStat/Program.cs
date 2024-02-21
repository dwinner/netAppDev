using System.Diagnostics;

namespace CurrentMemoryStat;

internal static class Program
{
   private static void Main()
   {
      long totalMemAllocated = 0;
      for (var i = 0; i < 200; i++)
      {
         totalMemAllocated += AllocateSomeNonreferencedMemory();
         var procName = Process.GetCurrentProcess().ProcessName;
         using var pcPb = new PerformanceCounter("Process", "Private Bytes", procName);
         var memoryUsed = GC.GetTotalMemory(false); // Change to true to force a collection before reporting used memory
         Console.WriteLine(
            $"Currently OS allocated: {pcPb.NextValue()}. Current GC reported {memoryUsed}. Allocated at some point {totalMemAllocated}.");
      }
   }

   private static long AllocateSomeNonreferencedMemory()
   {
      var loops = 64;
      var size = 1024;
      for (var i = 0; i < loops; i++)
      {
         var array = new int[size];
      }

      return loops * size * 4; // int is 32-bits (4 bytes)
   }
}