// Всевозможные перестановки слова

using System;
using System.Collections.Generic;
using System.Linq;

namespace AllSubstrings
{
   internal static class Program
   {
      private static void Main()
      {
         const string name = "LINQ";
         var words =
            Enumerable.Range(0, name.Length + 1).SelectMany(z => NGrams(name, z)).Distinct().Where(b => b.Length != 0);
         foreach (var word in words)
         {
            Console.WriteLine(word);
         }
      }

      private static IEnumerable<string> NGrams(string sentence, int q)
      {
         var total = sentence.Length - q;
         var tokens = new List<string>();
         for (var i = 0; i <= total; i++)
         {
            tokens.Add(sentence.Substring(i, q));
         }

         return tokens;
      }
   }
}