// Возможные формы слова из букв входного слова

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FormedWords
{
   internal static class Program
   {
      private const string T9WordLibrary = "T9.txt";

      private static void Main()
      {
         Func<string, Dictionary<char, int>> wordToHistTransformer =
            word => word.ToCharArray().ToLookup(w => w).ToDictionary(w => w.Key, w => w.Count());

         const string givenWord = "what";
         string total;
         using (var reader = new StreamReader(T9WordLibrary))
         {
            total = reader.ReadToEnd();
         }

         var allWords = Regex.Matches(total, "[a-z]+").Cast<Match>().Select(m => m.Value).Distinct().ToList();
         var forest = new Dictionary<string, Dictionary<char, int>>();
         allWords.ForEach(word => forest.Add(word, wordToHistTransformer(word)));
         var hist = wordToHistTransformer(givenWord);
         var scrabbleCheats =
            forest.Keys.Where(
               word =>
                  forest[word].Select(x => x.Key).All(x => hist.Select(h => h.Key).Contains(x)) &&
                  forest[word].All(x => hist[x.Key] - forest[word][x.Key] >= 0)).ToList();

         foreach (var s in scrabbleCheats.OrderBy(c => c.Length))
         {
            Console.WriteLine(s);
         }
      }
   }
}