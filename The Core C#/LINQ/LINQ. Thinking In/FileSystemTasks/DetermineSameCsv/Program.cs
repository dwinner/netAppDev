using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DetermineSameCsv
{
   internal static class Program
   {
      private static void Main()
      {
         Func<string, IEnumerable<string>> csvHeaders =
            fileName => File.ReadAllLines(fileName).First().Split(new[] { ',' }, StringSplitOptions.None);
         Func<string, IEnumerable<string>> csvBody =
            fileName => File.ReadAllLines(fileName).Skip(1).Where(line => line.Trim().Length != 0);
         Func<string, string, bool> isSameCsv =
            (firstFile, secondFile) =>
               csvHeaders(firstFile).All(headerItem => csvHeaders(secondFile).Contains(headerItem)) &&
               csvBody(firstFile).All(bodyItem => csvBody(secondFile).Contains(bodyItem));
      }
   }
}