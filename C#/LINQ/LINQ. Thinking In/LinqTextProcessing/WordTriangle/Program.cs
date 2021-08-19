using System;
using System.Linq;

namespace WordTriangle
{
   internal static class Program
   {
      private static void Main()
      {
         const string word = "umbrella";
         var wordTriangle =
            Enumerable.Range(1, word.Length)
               .Select(k => new string(word.ToCharArray().Take(k).ToArray()))
               .Concat(
                  Enumerable.Range(1, word.Length)
                     .Select(k => new string(word.ToCharArray().Take(word.Length - k).ToArray())))
               .Aggregate((a, b) => string.Format("{0}{1}{2}", a, Environment.NewLine, b));
         foreach (var c in wordTriangle)
         {
            Console.Write(c);
         }
      }
   }
}