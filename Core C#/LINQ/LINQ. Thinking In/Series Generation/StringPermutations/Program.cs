// Возможные перестановки букв в слове

using System;
using System.Collections.Generic;
using System.Linq;

namespace StringPermutations
{
   internal static class Program
   {
      private static void Main()
      {
         var perms = GeneratePartialPermutations("abc");
         Enumerable.Range(0, perms.Count - 1).ToList().ForEach(
            c =>
            {
               Enumerable.Range(0, perms.Count)
                  .ToList()
                  .ForEach(i => GeneratePartialPermutations(perms.ElementAt(i)).ToList().ForEach(p => perms.Add(p)));
               Enumerable.Range(0, perms.Count)
                  .ToList()
                  .ForEach(
                     i =>
                        GeneratePartialPermutations(new string(perms.ElementAt(i).ToCharArray().Reverse().ToArray()))
                           .ToList()
                           .ForEach(p => perms.Add(p)));
            });

         foreach (var perm in perms.OrderBy(p => p))
         {
            Console.WriteLine(perm);
         }
      }

      private static ISet<string> GeneratePartialPermutations(string word)
      {
         return
            new HashSet<string>(
               Enumerable.Range(0, word.Length).Select(i => word.Remove(i, 1).Insert(0, word[i].ToString())));
      }
   }
}