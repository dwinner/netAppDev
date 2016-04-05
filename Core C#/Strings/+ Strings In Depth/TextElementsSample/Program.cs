/**
 * Текстовые элементы строки
 */

using System;
using System.Globalization;

namespace TextElementsSample
{
   internal static class Program
   {
      private static void Main()
      {
         // Следующая строка содержит комбинированные символы
         var s = "a\u0304\u0308bc\u0327";
         SubstringByTextElements(s);
         Console.WriteLine();

         EnumTextElements(s);
         Console.WriteLine();

         EnumTextElementIndexes(s);
      }

      private static void EnumTextElementIndexes(string s)
      {
         var output = string.Empty;
         var characters = StringInfo.ParseCombiningCharacters(s);
         for (var i = 0; i < characters.Length; i++)
         {
            output += string.Format("Character {0} starts at index {1}{2}", i, characters[i], Environment.NewLine);
         }

         Console.WriteLine(output);
      }

      private static void EnumTextElements(string s)
      {
         var output = string.Empty;
         var charEnum = StringInfo.GetTextElementEnumerator(s);
         while (charEnum.MoveNext())
         {
            output += string.Format("Character at index {0} is '{1}'{2}", charEnum.ElementIndex,
               charEnum.GetTextElement(), Environment.NewLine);
         }

         Console.WriteLine(output);
      }

      private static void SubstringByTextElements(string s)
      {
         var output = string.Empty;
         var stringInfo = new StringInfo(s);
         for (var element = 0; element < stringInfo.LengthInTextElements; element++)
         {
            output += string.Format("Text element {0} is '{1}'{2}", element,
               stringInfo.SubstringByTextElements(element, 1), Environment.NewLine);
         }

         Console.WriteLine(output);
      }
   }
}