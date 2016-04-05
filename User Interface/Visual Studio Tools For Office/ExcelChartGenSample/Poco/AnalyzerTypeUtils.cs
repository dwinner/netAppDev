using System.Linq;
using System.Xml.Serialization;

namespace ExcelChartGenSample.Poco
{
   /// <summary>
   ///     Методы расширения для типов диагностик
   /// </summary>
   public static class AnalyzerTypeUtils
   {
      /// <summary>
      ///     Получает короткое имя для типа сообщений анализатора
      /// </summary>
      /// <param name="analyzerType">Тип сообщений анализатора</param>
      /// <returns>Короткое имя типа сообщений анализатора</returns>
      public static string GetShortName(AnalyzerType analyzerType)
      {
         return
             analyzerType.GetType()
                 .GetField(analyzerType.ToString())
                 .GetCustomAttributes(typeof(XmlEnumAttribute), false)
                 .Cast<XmlEnumAttribute>()
                 .First()
                 .Name;
      }
   }
}