// Создание подсказок по завершению слова

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpellingCorrection
{
   internal static class Program
   {
      private const string T9WordLibrary = "T9.txt";
      private static Dictionary<string, int> _numberWords;

      private static void Main()
      {
         var reader = new StreamReader(T9WordLibrary);
         var total = reader.ReadToEnd();
         reader.Close();

         _numberWords = Train(Regex.Matches(total, "[a-z]+").Cast<Match>().Select(m => m.Value.ToLower()));
         const string word = "mysgtry"; // скорее всего имелось ввиду слово "mystery"
         var corrections = Correct(word).Distinct().OrderByDescending(x => x.Length);
         foreach (var correction in corrections)
         {
            Console.WriteLine(correction);
         }
      }

      private static Dictionary<string, int> Train(IEnumerable<string> features)
      {
         var model = new Dictionary<string, int>();
         features.ToList().ForEach(f =>
         {
            if (model.ContainsKey(f))
            {
               model[f] += 1;
            }
            else
            {
               model.Add(f, 1);
            }
         });

         return model;
      }

      private static IEnumerable<string> KnownEdits2(string word)
      {         
         return Edits1(word).SelectMany(e1 => Edits1(e1).Where(x => _numberWords.ContainsKey(x)));
      }

      private static IEnumerable<string> Known(IEnumerable<string> words)
      {
         return words.Intersect(_numberWords.Select(v => v.Key));
      }

      private static IEnumerable<string> Edits1(string word)
      {
         char[] alphabet =
         {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
            't', 'u', 'v', 'w', 'x', 'y', 'z'
         };
         var splits =
            Enumerable.Range(1, word.Length - 1)
               .Select(i => new { First = word.Substring(0, i), Second = word.Substring(i + 1) })
               .ToArray();

         var deletes =
            splits.Where(split => !string.IsNullOrEmpty(split.Second))
               .Select(split => string.Format("{0}{1}", split.First, split.Second.Substring(1)));
         var transposes =
            splits.Where(split => split.Second.Length > 1)
               .Select(
                  split =>
                     string.Format("{0}{1}{2}{3}", split.First, split.Second[1], split.Second[0],
                        split.Second.Substring(2)));
         var replaces =
            splits.Where(split => !string.IsNullOrEmpty(split.Second))
               .SelectMany(
                  split => alphabet.Select(c => string.Format("{0}{1}{2}", split.First, c, split.Second.Substring(1))));
         var inserts =
            splits.Where(split => !string.IsNullOrEmpty(split.Second))
               .SelectMany(split => alphabet.Select(c => string.Format("{0}{1}{2}", split.First, c, split.Second)));

         return deletes.Union(transposes).Union(replaces).Union(inserts);
      }

      private static IEnumerable<string> Correct(string word)
      {
         var candidates = new List<string>();
         candidates.AddRange(Known(new List<string> { word }));
         candidates.AddRange(Known(Edits1(word)));         
         candidates.AddRange(KnownEdits2(word));
         candidates.Add(word);
         return candidates.Where(c => _numberWords.ContainsKey(c)).OrderByDescending(c => _numberWords[c]);
      }
   }
}