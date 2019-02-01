// Генерирование подпредложений

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AllSubSequences
{
   internal static class Program
   {
      private const string T9Library = "T9.txt";

      private static void Main()
      {
         List<string> allWords;
         using (var reader = new StreamReader(T9Library))
         {
            allWords = Regex.Matches(reader.ReadToEnd(), "[a-z]+").Cast<Match>().Select(m => m.Value).ToList();
         }

         const string bigWord = "awesome";
         var subSequences =
            (from smallWord in allWords
             let q = smallWord.ToCharArray().Select(x => bigWord.ToCharArray().ToList().LastIndexOf(x))
             where
                q.All(x => x != -1) &&
                q.Take(q.Count() - 1)
                   .Select((x, i) => new { CurrentIndex = x, NextIndex = q.ElementAt(i + 1) })
                   .All(x => x.NextIndex - x.CurrentIndex > 0)
             select smallWord).ToList();

         subSequences.ForEach(Console.WriteLine);
      }
   }
}