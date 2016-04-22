/**
 * Список дисков в системе
 */

using System;
using System.IO;

namespace EnumDrives
{
   class Program
   {
      static void Main()
      {
         DriveInfo[] drives = DriveInfo.GetDrives();
         foreach (DriveInfo info in drives)
         {
            Console.WriteLine("Name: {0} (RootDirectory: {1}", info.Name, info.RootDirectory);
            Console.WriteLine("DriveType: {0}", info.DriveType);
            Console.WriteLine("IsReady: {0}", info.IsReady);
            // Note: при попытке чтения с диска, который не готов, будет возбуждено исключение
            if (info.IsReady)
            {
               Console.WriteLine("VolumeLabel: {0}", info.VolumeLabel);
               Console.WriteLine("DriveFormat: {0}", info.DriveFormat);
               Console.WriteLine("TotalSize: {0:N0}", info.TotalSize);
               Console.WriteLine("TotalFreeSpace: {0:N0}", info.TotalFreeSpace);
               Console.WriteLine("AvailableFreeSpace: {0:N0}", info.AvailableFreeSpace);
            }

            Console.WriteLine();
         }

         Console.ReadKey();
      }
   }
}
