using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace LinuxHeadTailCommands
{
   internal static class Program
   {
      private static void Main()
      {
         Func<IEnumerable<string>, int, IEnumerable<string>> takeLast =
            (list, count) => list.Skip(list.Count() - count);
         Func<string, int, IEnumerable<string>> head =
            (fileName, lineCount) => File.ReadAllLines(fileName).Take(lineCount);
         Func<string, int, IEnumerable<string>> tail =
            (fileName, lineCount) => takeLast(File.ReadAllLines(fileName), lineCount);
      }
   }
}