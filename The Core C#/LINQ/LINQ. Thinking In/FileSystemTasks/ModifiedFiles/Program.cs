using System;
using System.IO;
using System.Linq;
using MoreLinq;

namespace ModifiedFiles
{
   static class Program
   {
      private static readonly string[] ProgramFiles =
         Directory.GetFiles(@"C:\Program Files", "*.*", SearchOption.AllDirectories);

      static void Main()
      {
         var lastWeekWritten = ProgramFiles.Select(dir => new FileInfo(dir))
            .OrderByDescending(fileInfo => fileInfo.LastWriteTime)
            .Select(fileInfo => new { Name = fileInfo.FullName, LastModifiedTime = fileInfo.LastWriteTime })
            .Where(arg => arg.LastModifiedTime.AddDays(7).CompareTo(DateTime.Today) >= 0);
         lastWeekWritten.ForEach(obj => Console.WriteLine("{0}: {1}", obj.Name, obj.LastModifiedTime.ToString("g")));
      }
   }
}