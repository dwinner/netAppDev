/**
 * Варианты эмуляции логикой управления в контексте потоков данных
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ifElseSwitchLinking
{
   internal static class Program
   {
      private static void Main()
      {
         var walker = new DirectoryWalker(Console.WriteLine);
         walker.WalkAsync(@"D:\repositories").Wait();
         var files = GetFiles(@"D:\repositories").ToList();
         var dwFiles = new List<string>();
         walker = new DirectoryWalker(f => dwFiles.Add(f));
         walker.WalkAsync(@"D:\repositories").Wait();
         Console.WriteLine("{0} == {1} : {2}", dwFiles.Count, files.Count, dwFiles.Count == files.Count);
      }

      private static IEnumerable<string> GetFiles(string path)
      {
         var dir = new DirectoryInfo(path);
         foreach (var file in dir.GetFiles())
         {
            yield return dir.FullName;
         }

         foreach (var file in dir.GetDirectories().SelectMany(subDir => GetFiles(subDir.FullName)))
         {
            yield return file;
         }
      }
   }
}