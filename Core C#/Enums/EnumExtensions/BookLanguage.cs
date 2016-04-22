namespace EnumExtensions
{
   /// <summary>
   /// Перечисление, снабженное атрибутами культуры
   /// </summary>
   enum BookLanguage
   {
      None = 0,

      [Culture("en-US")]
      [Culture("en-UK")]
      English = 1,

      [Culture("es-MX")]
      [Culture("es-ES")]
      Spanish = 2,

      [Culture("it-IT")]
      Italian = 3,

      [Culture("fr-FR")]
      [Culture("fr-BE")]
      French = 4,
   }
}
