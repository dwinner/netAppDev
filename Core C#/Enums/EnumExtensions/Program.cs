/**
 * Добавление метаданных к перечислению
 */

using System;

namespace EnumExtensions
{
   class Program
   {
      static void Main()
      {
         PrintCultures(BookLanguage.English);
         PrintCultures(BookLanguage.Spanish);

         Console.ReadKey(true);
      }

      static void PrintCultures(BookLanguage language)
      {
         Console.WriteLine("Cultures for {0}:", language);
         foreach (string culture in language.GetCultures())
         {
            Console.WriteLine("\t" + culture);
         }
      }
   }
}
