using System.Collections.Generic;

namespace EnumExtensions
{
   /// <summary>
   ///    Расширения для перечисления BookLanguage
   /// </summary>
   internal static class CultureExtensions
   {
      /// <summary>
      ///    Получение культур из переданных атрибутов
      /// </summary>
      /// <param name="language">Перечисление BookLanguage</param>
      /// <returns>Массив культур</returns>
      public static IEnumerable<string> GetCultures(this BookLanguage language)
      {
         // Note: Этот код будет работать только для жанров с единственным значением
         var cultureAttributes =
            (CultureAttribute[])language.GetType().GetField(language.ToString())
               .GetCustomAttributes(typeof(CultureAttribute), false);
         var cultures = new string[cultureAttributes.Length];
         for (var i = 0; i < cultureAttributes.Length; i++)
         {
            cultures[i] = cultureAttributes[i].Culture;
         }

         return cultures;
      }
   }
}