/**
 * Изменение порядка слов в строке на обратный
 */

using System;
using System.Collections.Generic;

namespace ReverseWords
{
   class Program
   {
      static void Main()
      {
         const string original = "But, in a larger sense, we can not dedicate—we can not consecrate—we can not hallow—this ground.";

         Console.WriteLine("Original: {0}", original);
         Console.WriteLine("Reversed: {0}", ReverseWords(original));

         Console.ReadKey();
      }

      static string ReverseWords(string original)
      {
         // Вначале преобразуем строку в символьный массив, поскольку
         // нам придется сильно модифицировать его
         char[] chars = original.ToCharArray();
         ReverseCharArray(chars, 0, chars.Length - 1);

         // Теперь найдем последовательность символов и
         // обратим каждую группу в отдельности
         int wordStart = 0;
         while (wordStart < chars.Length)
         {
            // Пропускаем небуквенные символы
            while (wordStart < chars.Length - 1 && !char.IsLetter(chars[wordStart]))
               wordStart++;
            // Находим конец слова
            int wordEnd = wordStart;
            while (wordEnd < chars.Length - 1 && char.IsLetter(chars[wordEnd + 1]))
               wordEnd++;
            // Меняем порядок символов в этой группе
            if (wordEnd > wordStart)
            {
               ReverseCharArray(chars, wordStart, wordEnd);
            }
            wordStart = wordEnd + 1;
         }
         return new string(chars);
      }

      static void ReverseCharArray(IList<char> chars, int left, int right)
      {
         int l = left, r = right;
         while (l < r)
         {
            char temp = chars[l];
            chars[l] = chars[r];
            chars[r] = temp;
            l++;
            r--;
         }
      }
   }
}
