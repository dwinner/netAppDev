using System;
using System.IO;
using System.Linq;
using MoreLinq;

namespace IdenticalContentFiles
{
   internal static class Program
   {
      private static readonly string[] ProgramFiles =
         Directory.GetFiles(@"C:\Program Files", "*.*", SearchOption.AllDirectories);

      private static void Main()
      {
         var similarFiles =
            ProgramFiles.Where(file => file.EndsWith(".txt"))
               .Select(file => new { FileName = file, ContentHash = File.ReadAllText(file).GetHashCode() })
               .ToLookup(arg => arg.ContentHash)
               .Where(group => group.Count() >= 2);
         similarFiles.ForEach(group =>
         {
            Console.WriteLine("Content hash: {0}", @group.Key);
            group.ForEach(obj => Console.WriteLine("\t{0}", obj.FileName));
         });
      }
   }
}