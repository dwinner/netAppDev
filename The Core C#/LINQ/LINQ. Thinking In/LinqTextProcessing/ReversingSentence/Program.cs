using System;
using System.Linq;

namespace ReversingSentence
{
   internal static class Program
   {
      private static void Main()
      {
         const string line = "nothing know I";
         var reversedSentence = line.Split(' ').Aggregate((a, b) => string.Format("{0} {1}", b, a));
         Console.WriteLine(reversedSentence);
      }
   }
}