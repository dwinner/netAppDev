using System;
using System.IO;
using System.Linq;

namespace DefaultAndNamedParams
{
   class Program
   {
      static void Main(string[] args)
      {
         ShowFolders();
         ShowFolders(@"C:\");

         ShowFolders(showFullPath: false, root: @"C:\Windows");

         Console.ReadKey();
      }

      static void ShowFolders(string root = @"C:\", bool showFullPath = false)
      {
         foreach (string output in
            Directory.EnumerateDirectories(root).Select(folder => showFullPath ? folder : Path.GetFileName(folder)))
         {
            Console.WriteLine(output);
         }
      }
   }
}
