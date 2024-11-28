using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pinning
{
   internal static class Program
   {
      private static void Main()
      {
         // FileSystemWatcher implicitly pins memory internally.
         var tempPath = Path.GetTempPath();
         using (var fileSysWatcher = new FileSystemWatcher(tempPath, "*.*"))
         {
            fileSysWatcher.NotifyFilter = NotifyFilters.LastWrite;

            fileSysWatcher.Created += (sender, e) => Console.WriteLine($"Created: {e.Name}");
            fileSysWatcher.Changed += (sender, e) => Console.WriteLine($"Changed: {e.Name}");
            fileSysWatcher.Deleted += (sender, e) => Console.WriteLine($"Deleted: {e.Name}");

            fileSysWatcher.EnableRaisingEvents = true;

            Task.Run(() =>
            {
               var tempFile = Path.GetTempFileName();
               try
               {
                  while (true)
                  {
                     File.WriteAllText(tempFile, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                     Thread.Sleep(100);
                  }
               }
               finally
               {
                  File.Delete(tempFile);
               }
            });
            Console.WriteLine("Press Any key to exit");
            Console.ReadKey();
         }
      }
   }
}