/**
 * Интернирование строк
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace StringInterning
{
   internal static class Program
   {
      private const string Word = "";
      private static readonly string[] WordArray = { "" };

      private static void Main()
      {
         // Версия с обычным сравнением
         var i = NumTimesWordAppears(WordArray, s => Word.Equals(s, StringComparison.Ordinal));

         Console.WriteLine(i);

         // Версия с интернированием строк
         i = NumTimesWordAppears(WordArray.Affect(string.Intern),
            item => ReferenceEquals(Word, item));

         Console.WriteLine(i);
      }

      private static int NumTimesWordAppears(IEnumerable<string> aWordList, Func<string, bool> cmpFunc)
      {
         return aWordList.Count(cmpFunc);
      }
   }

   public static class StringExtensions
   {
      /// <exception cref="Exception">A delegate callback throws an exception.</exception>
      public static T[] Affect<T>(this T[] array, Func<T, T> affectFunc)
      {
         for (var i = 0; i < array.Length; i++)
         {
            array[i] = affectFunc(array[i]);
         }

         return array;
      }
   }
}