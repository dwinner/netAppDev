// Вычисление анаграмм без overhead'ов сортировки

using System;
using System.Linq;

namespace CalculatingAnagrams2
{
   internal static class Program
   {
      private const string Phrase1 = "the eyes";
      private const string Phrase2 = "they see";

      private static void Main()
      {
         var charHistogram1 = Phrase1.ToCharArray()
            .Where(char.IsLetterOrDigit)
            .ToLookup(p => p)
            .ToDictionary(p => p.Key, p => p.Count());
         var charHistogram2 = Phrase2.ToCharArray()
            .Where(char.IsLetterOrDigit)
            .ToLookup(p => p)
            .ToDictionary(p => p.Key, p => p.Count());
         var isAnagram = charHistogram1.All(d => charHistogram2[d.Key] == charHistogram1[d.Key]);
         Console.WriteLine(isAnagram);
      }
   }
}