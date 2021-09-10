// Множество всех перестановок слова

using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerSet
{
   internal static class Program
   {
      private static void Main()
      {
         const string word = "abc";
         var perms = GeneratePartialPermutations(word);
         Enumerable.Range(0, word.Length).ToList().ForEach(x => Enumerable.Range(0, word.Length).ToList().ForEach(z =>
         {
            perms.Add(perms.ElementAt(x).Substring(0, z));
            perms.Add(perms.ElementAt(x).Substring(z + 1));
         }));

         var dump =
            perms.Select(p => new string(p.ToCharArray().OrderBy(x => x).ToArray())).Distinct().OrderBy(p => p.Length);
         foreach (var str in dump)
         {
            Console.WriteLine(str);
         }
      }

      private static ISet<string> GeneratePartialPermutations(string word)
      {
         return
            new HashSet<string>(
               Enumerable.Range(0, word.Length)
                  .Select(startIndex => word.Remove(startIndex, 1).Insert(0, word[startIndex].ToString())));
      }
   }
}