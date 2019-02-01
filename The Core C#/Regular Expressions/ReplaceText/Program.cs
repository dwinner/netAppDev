/**
 * Замена фрагментов текста, обратные вызовы при замене.
 */

using System;
using System.Text.RegularExpressions;

namespace ReplaceText
{
   class Program
   {
      static void Main()
      {
         const string source = "We few, we happy few, we band of brothers";
         Console.WriteLine("source: {0}", source);
         Console.WriteLine("Replace word after we");
         var regex = new Regex("[wW]e\\s[a-zA-Z]+");
         string result = regex.Replace(source, "we <something>"); // Простая замена
         Console.WriteLine("result: {0}", result);

         Console.WriteLine("Swap we with next word:");

         // Поместить слово, идущее после "we", в отдельную группу,
         // чтобы его можно было извлечь позже
         regex = new Regex("[wW]e\\s(?<OtherWord>[a-zA-Z]+)");

         // Передать наш метод делегату MatchEvaluator
         result = regex.Replace(source, match => (match.Groups["OtherWord"].Value + " we")); // Поменять местами " we" и слово, которое за ним следует
         Console.WriteLine("result: {0}", result);

         Console.ReadKey();
      }
   }
}
