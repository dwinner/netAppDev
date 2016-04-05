using System;
using System.IO;

namespace DriveInfoApp
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with DriveInfo *****\n");

         // Получаем все диски.
         DriveInfo[] myDrives = DriveInfo.GetDrives();

         // Печатаем информацию о них.
         foreach (DriveInfo driveInfo in myDrives)
         {
            Console.WriteLine("Name: {0}", driveInfo.Name);
            Console.WriteLine("Type: {0}", driveInfo.DriveType);

            // Готов ли диск к использованию.
            if (driveInfo.IsReady)
            {
               Console.WriteLine("Free space: {0}", driveInfo.TotalFreeSpace);
               Console.WriteLine("Format: {0}", driveInfo.DriveFormat);
               Console.WriteLine("Label: {0}", driveInfo.VolumeLabel);
            }
            Console.WriteLine();
         }
         Console.ReadLine();
      }
   }

}
