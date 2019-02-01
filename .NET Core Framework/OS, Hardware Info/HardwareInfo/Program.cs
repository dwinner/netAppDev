/**
 * Получение информации об аппаратных средствах
 */

using System;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace HardwareInfo
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("Machine: {0}", Environment.MachineName);
         Console.WriteLine("# of processors (logical): {0}", Environment.ProcessorCount);
         Console.WriteLine("# of processors (physical): {0}", CountPhysicalProcessors());
         Console.WriteLine("RAM installed: {0:N0} bytes", CountPhysicalMemory());
         Console.WriteLine("Is OS 64-bit? {0}", Environment.Is64BitOperatingSystem);
         Console.WriteLine("Is process 64-bit? {0}", Environment.Is64BitProcess);
         Console.WriteLine("Little-endian: {0}", BitConverter.IsLittleEndian);

         foreach (var screen in Screen.AllScreens)
         {
            Console.WriteLine("Screen {0}", screen.DeviceName);
            Console.WriteLine("\tPrimary {0}", screen.Primary);
            Console.WriteLine("\tBounds: {0}", screen.Bounds);
            Console.WriteLine("\tWorking Area: {0}", screen.WorkingArea);
            Console.WriteLine("\tBitsPerPixel: {0}", screen.BitsPerPixel);
         }
      }

      private static int CountPhysicalProcessors()
      {
         var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
         var managementObjectCollection = searcher.Get();
         try
         {
            return
            (from ManagementObject manObj in managementObjectCollection
               select manObj["NumberOfProcessors"]
               into numOfProcObj
               select (int) numOfProcObj).FirstOrDefault();
         }
         catch (ManagementException managementEx)
         {
            Console.WriteLine(
               "Error code: {0}. Error info: {1}", managementEx.ErrorCode, managementEx.ErrorInformation);
         }

         return 0;
      }

      private static long CountPhysicalMemory()
      {
         var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
         var manObjColl = searcher.Get();
         try
         {
            return
            (from ManagementObject manObj in manObjColl
               select manObj["Capacity"]
               into capacityObj
               select (long) capacityObj).Sum();
         }
         catch (ManagementException managementEx)
         {
            Console.WriteLine(
               "Error code: {0}. Error info: {1}", managementEx.ErrorCode, managementEx.ErrorInformation);
         }

         return 0L;
      }
   }
}