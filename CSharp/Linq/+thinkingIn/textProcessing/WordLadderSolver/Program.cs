// Построение маршрута к исклмлму слову

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordLadderSolver
{
   internal static class Program
   {
      private const string T9WordLibrary = "T9.txt";

      private static void Main()
      {
         var transitions = new List<string>();
         var allWords = new List<string>();

         string total;
         using (var t9Reader = new StreamReader(T9WordLibrary))
         {
            total = t9Reader.ReadToEnd();
         }

         var start = "myth";
         const string end = "fact";

         transitions.Add(start);
         allWords.AddRange(total.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
         allWords = allWords.Where(word => word.Length == start.Length).ToList();
         allWords.Add(end);
         var wordEditDistanceMap = allWords.ToLookup(w => w)
            .ToDictionary(w => w.Key, w => allWords.Where(a => HammimgDistance(a, w.Key) == 1).ToList());
         var noPath = false;
         var currentList = new List<string>();
         do
         {
            var currents =
               wordEditDistanceMap[start].Where(
                  word => HammimgDistance(word, end) == wordEditDistanceMap[start].Min(c => HammimgDistance(end, c)))
                  .ToArray();
            do
            {
               foreach (var c in currents)
               {
                  if (!currentList.Contains(c))
                  {
                     currentList.Add(c);
                     break;
                  }

                  if (currentList.Count == 1 && currentList.Contains(c))
                  {
                     Console.WriteLine("There is no such path!");
                     noPath = true;
                     break;
                  }
               }

               if (noPath)
                  break;
            } while (currentList.Count == 0);

            if (noPath)
               break;

            transitions.Add(currentList[currentList.Count - 1]);
            if (transitions.Count >= 2 && transitions[transitions.Count - 2] == transitions.Last())
            {
               Console.WriteLine("There is no such path");
               noPath = true;
               break;
            }

            start = currentList[currentList.Count - 1];
         } while (!start.Equals(end));

         if (!noPath)
         {
            transitions.ForEach(Console.WriteLine);
         }
      }

      private static int HammimgDistance(string first, string second)
      {
         return first.ToCharArray().Where((f, i) => second[i] != f).Count();
      }
   }
}