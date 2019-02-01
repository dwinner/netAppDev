using System;
using System.IO;

namespace MyDirectoryWatcher
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** The Amazing File Watcher App *****\n");

         // Установим путь, за которым будем наблюдать
         var watcher = new FileSystemWatcher();
         try
         {
            Directory.CreateDirectory(@"MyFolder");
            watcher.Path = @"MyFolder";
         }
         catch (ArgumentException ex)
         {
            Console.WriteLine(ex.Message);
            Console.ReadKey();
            return;
         }

         // За чем нужно наблюдать
         watcher.NotifyFilter = NotifyFilters.LastAccess
           | NotifyFilters.LastWrite
           | NotifyFilters.FileName
           | NotifyFilters.DirectoryName;

         // Наблюдаем только за txt-файлами.
         watcher.Filter = "*.txt";

         // Что будет происходит, когда файл изменился, был создан или удален.
         watcher.Changed += (s, e) =>
         {
            Console.WriteLine("File: {0} {1}!", e.FullPath, e.ChangeType);
         };
         watcher.Created += (s, e) =>
         {
            Console.WriteLine("File: {0} {1}!", e.FullPath, e.ChangeType);
         };
         watcher.Deleted += (s, e) =>
         {
            Console.WriteLine("File: {0} {1}!", e.FullPath, e.ChangeType);
         };

         watcher.Renamed += (s, e) =>
         {
            // Что будет происходить, если файл был переименован.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
         };

         // Начнём наблюдение 
         watcher.EnableRaisingEvents = true;

         // Ждем, пока позьзователь не завершит программу.
         Console.WriteLine(@"Press 'q' to quit app.");
         while (Console.Read() != 'q') ;
      }
   }
}
