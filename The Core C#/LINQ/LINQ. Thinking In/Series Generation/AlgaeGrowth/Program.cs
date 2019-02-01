using System;
using System.Linq;

namespace AlgaeGrowth
{
   internal static class Program
   {
      private static void Main()
      {
         var algae = "A";
         Func<string, string> transformA = sourceStr => sourceStr.Replace("A", "AB");
         Func<string, string> markBs = sourceStr => sourceStr.Replace("B", "[B]");
         Func<string, string> transformB = sourceStr => sourceStr.Replace("[B]", "A");
         const int length = 7;
         Enumerable.Range(1, length).ToList().ForEach(k => algae = transformB(transformA(markBs(algae))));
         Console.WriteLine("7th algae iteration: {0}", algae);
      }
   }
}