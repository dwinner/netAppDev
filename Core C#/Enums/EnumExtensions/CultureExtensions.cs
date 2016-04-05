namespace EnumExtensions
{
   /// <summary>
   /// Расширения для перечисления BookLanguage
   /// </summary>
   static class CultureExtensions
   {
      /// <summary>
      /// Получение культур из переданных атрибутов
      /// </summary>
      /// <param name="language">Перечисление BookLanguage</param>
      /// <returns>Массив культур</returns>
      public static string[] GetCultures(this BookLanguage language)
      {
         // Note: Этот код будет работать только для жанров с единственным значением
         CultureAttribute[] cultureAttributes =
            (CultureAttribute[])language.GetType().GetField(language.ToString()).GetCustomAttributes(typeof(CultureAttribute), false);
         string[] cultures = new string[cultureAttributes.Length];
         for (int i = 0; i < cultureAttributes.Length; i++)
         {
            cultures[i] = cultureAttributes[i].Culture;
         }
         return cultures;
      }
   }
}
