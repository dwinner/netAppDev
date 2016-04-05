// Вычисление анаграмм

using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatingAnagrams
{
   internal static class Program
   {
      private const string Phrase1 = "the eyes";
      private const string Phrase2 = "they see";

      private static void Main()
      {
         var isAnagram =
            Phrase1.ToCharArray()
               .Where(char.IsLetterOrDigit)
               .OrderBy(p => p)
               .SequenceEqual(Phrase2.ToCharArray().Where(char.IsLetterOrDigit).OrderBy(p => p),
                  EqualityComparer<char>.Default);
         Console.WriteLine(isAnagram);
      }
   }
}