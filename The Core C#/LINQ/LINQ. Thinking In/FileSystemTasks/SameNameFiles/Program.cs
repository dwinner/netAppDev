using System;
using System.IO;
using System.Linq;
using MoreLinq;

namespace SameNameFiles
{
   internal static class Program
   {
      private static readonly string[] ProgramFiles =
         Directory.GetFiles(@"C:\Program Files", "*.*", SearchOption.AllDirectories);

      private static void Main()
      {
         var sameNames =
            ProgramFiles.Select(dir => new FileInfo(dir))
               .Select(fileInfo => new { FileName = fileInfo.Name, Directory = fileInfo.DirectoryName })
               .ToLookup(arg => arg.FileName)
               .Where(group => group.Count() >= 2);
         sameNames.ForEach(group =>
         {
            Console.WriteLine("File name: {0}. Count: {1}", group.Key, group.Count());
         });
      }
   }
}