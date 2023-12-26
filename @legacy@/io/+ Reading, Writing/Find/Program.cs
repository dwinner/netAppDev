/**
 * Поиск файла/файлов или каталога/каталогов
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Find
{
   class Program
   {
      static bool _folderOnly;
      static string _startFolder;
      static string _searchTerm;

      static void Main(string[] args)
      {
         if (!ParseArgs(args))
         {
            PrintUsage();
            return;
         }

         Console.WriteLine("Searching {0} for \"{1}\" {2}",
             _startFolder, _searchTerm, _folderOnly ? "(folders only)" : "");

         DoSearch();
      }

      private static void DoSearch()
      {
         DirectoryInfo directoryInfo = new DirectoryInfo(_startFolder);

         // IEnumerable<DirectoryInfo> enumerateDirectories = directoryInfo.EnumerateDirectories("*", SearchOption.AllDirectories);                  
         DirectoryInfo[] directories = directoryInfo.GetDirectories(_searchTerm, SearchOption.AllDirectories);

         int numResults = directories.Length;
         PrintSearchResults(directories);
         if (!_folderOnly)
         {
            FileInfo[] files = directoryInfo.GetFiles(_searchTerm, SearchOption.AllDirectories);
            PrintSearchResults(files);
            numResults += files.Length;
         }

         Console.WriteLine("{0:N0} results found", numResults);
      }

      private static void PrintSearchResults(IEnumerable<DirectoryInfo> directories)
      {
         foreach (DirectoryInfo info in directories.Where(info => info.Parent != null))
         {
            Console.WriteLine("{0}\t{1}\t{2}", info.Name, info.Parent.FullName, "D");
         }
      }

      private static void PrintSearchResults(IEnumerable<FileInfo> files)
      {
         foreach (FileInfo fileInfo in files)
         {
            Console.WriteLine("{0}\t{1}\t{2}", fileInfo.Name, fileInfo.DirectoryName, "F");
         }
      }

      static void PrintUsage()
      {
         Console.WriteLine("Usage: Find [-directory] SearchTerm StartFolder");
         Console.WriteLine("Ex: Find -directory code D:\\Projects");
         Console.WriteLine("* wildcards are accepted");
      }

      static bool ParseArgs(IList<string> args)
      {
         if (args.Count < 2)
         {
            return false;
         }
         if (string.CompareOrdinal(args[0], "-directory") == 0)
         {
            _folderOnly = true;
            if (args.Count < 3)
               return false;
         }
         _startFolder = args[args.Count - 1];
         _searchTerm = args[args.Count - 2];
         return true;
      }
   }
}
