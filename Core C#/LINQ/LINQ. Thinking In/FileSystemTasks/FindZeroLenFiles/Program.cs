using System;
using System.IO;
using System.Linq;
using MoreLinq;

namespace FindZeroLenFiles
{
   internal static class Program
   {
      private static readonly string[] ProgramFiles =
         Directory.GetFiles(@"C:\Program Files", "*.*", SearchOption.AllDirectories);

      private static void Main()
      {
         var deadFiles =
            ProgramFiles
               .Select(dir => new FileInfo(dir))
               .Where(fileInfo => fileInfo.Length == 0);
         deadFiles.ForEach(fi => Console.WriteLine(fi.FullName));
      }
   }
}