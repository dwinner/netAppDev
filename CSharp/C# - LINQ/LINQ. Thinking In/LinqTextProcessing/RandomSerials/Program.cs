using System;
using System.Linq;

namespace RandomSerials
{
   internal static class Program
   {
      private static void Main()
      {
         Enumerable.Range(65, 26)
            .Select(e => ((char)e).ToString())
            .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            .OrderBy(e => Guid.NewGuid())
            .Take(8)
            .ToList()
            .ForEach(Console.Write);
         Console.WriteLine();
      }
   }
}