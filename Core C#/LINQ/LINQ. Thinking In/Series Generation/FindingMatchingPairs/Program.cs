// Поиск "созвучных" слов (Рудиментарный алгоритм предполагает, что у созвучных слов совпадают три последние буквы)

using System;
using System.Collections.Generic;
using System.Linq;

namespace FindingMatchingPairs
{
   internal static class Program
   {
      private static void Main()
      {
         string[] words1 =
         {
            "orange", "herbal", "rubble", "indicative", "mandatory", "brush", "golden", "diplomatic",
            "pace"
         };
         string[] words2 = { "verbal", "rush", "pragmatic", "story", "race", "bubble", "olden" };

         Func<string, string, bool> mightRhyme =
            (a, b) =>
               a[a.Length - 1] == b[b.Length - 1] && a[a.Length - 2] == b[b.Length - 2] &&
               a[a.Length - 3] == b[b.Length - 3];
         var dump =
            words1.Select(w => new KeyValuePair<string, string>(w, words2.FirstOrDefault(wo => mightRhyme(w, wo))))
               .Where(w => !string.IsNullOrEmpty(w.Value));
         foreach (var pair in dump)
         {
            Console.WriteLine("{0} {1}", pair.Key, pair.Value);
         }

         // Альтернативный вариант
         var maybeRhymes =
            words1.Concat(words2)
               .ToLookup(w => w.Substring(w.Length - 3))
               .Where(w => w.Count() >= 2)
               .Select(w => w.Aggregate((m, n) => string.Format("{0}, {1}", m, n)));
         foreach (var maybeRhyme in maybeRhymes)
         {
            Console.WriteLine(maybeRhyme);
         }
      }
   }
}