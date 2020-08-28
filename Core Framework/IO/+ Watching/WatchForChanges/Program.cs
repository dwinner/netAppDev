/**
 * Отслеживание изменений в файловой системе
 */

using System;
using System.IO;
using System.Threading;

namespace WatchForChanges
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length < 1)
         {
            Console.WriteLine("Usage: WatchForChanges [FolderToWatch]");
            return;
         }

         FileSystemWatcher watcher = new FileSystemWatcher
            {
               Path = args[0],
               NotifyFilter = NotifyFilters.Size |
                              NotifyFilters.FileName |
                              NotifyFilters.DirectoryName |
                              NotifyFilters.CreationTime,
               Filter = "*.*"
            };
         watcher.Changed += watcher_Change;
         watcher.Created += watcher_Change;
         watcher.Deleted += watcher_Change;
         watcher.Renamed += watcher_Renamed;

         // Note. Синхронный вариант: WaitForChangedResult waitForChanged = watcher.WaitForChanged(WatcherChangeTypes.All);

         Console.WriteLine("Manipulate files in {0} to see activity...", args[0]);

         watcher.EnableRaisingEvents = true;

         while (true)
         {
            Thread.Sleep(1000);
         }
      }

      static void watcher_Change(object sender, FileSystemEventArgs e)
      {
         Console.WriteLine("{0} changed ({1})", e.Name, e.ChangeType);
      }

      static void watcher_Renamed(object sender, RenamedEventArgs e)
      {
         Console.WriteLine("{0} renamed to {1}", e.OldName, e.Name);
      }
   }
}
