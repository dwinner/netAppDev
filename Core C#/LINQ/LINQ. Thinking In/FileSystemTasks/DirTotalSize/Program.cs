using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;

namespace DirTotalSize
{
   static class Program
   {
      private static readonly IList<string> UserFolderFiles = new List<string>();         

      static Program()
      {
         try
         {
            UserFolderFiles = Directory.GetFiles(@"C:\Users", "*.*", SearchOption.AllDirectories);
         }
         catch (UnauthorizedAccessException)
         {            
         }
      }

      static void Main()
      {
         var totalSizes =
            UserFolderFiles.Select(file => new FileInfo(file))
               .Select(fileInfo => new {Directory = fileInfo.DirectoryName, FileSize = fileInfo.Length})
               .ToLookup(arg => arg.Directory)
               .Select(
                  dirGroup =>
                     new
                     {
                        Directory = dirGroup.Key,
                        TotalSizeInMb = Math.Round(dirGroup.Select(arg => arg.FileSize).Sum()/1048576D, 2)
                     })
               .OrderByDescending(arg => arg.TotalSizeInMb);
         totalSizes.ForEach(obj => Console.WriteLine("{0}: {1}Mb", obj.Directory, obj.TotalSizeInMb));
      }
   }
}