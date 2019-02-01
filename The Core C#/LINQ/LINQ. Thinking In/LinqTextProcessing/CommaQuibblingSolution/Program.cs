using System;
using System.Linq;

namespace CommaQuibblingSolution
{
   internal static class Program
   {
      private static void Main()
      {
         string[] input = { "ABC", "DEF", "G", "H" };
         var result = string.Format("{{{0} and {1}}}",
            input.Take(input.Length - 1).Aggregate((f, s) => string.Format("{0}, {1}", f, s)), input.Last());
         Console.WriteLine(result);
      }
   }
}