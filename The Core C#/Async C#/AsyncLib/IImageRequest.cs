using System.Collections.Generic;
using System.Net;

namespace AsyncLib
{
   /// <summary>
   /// Интерфейс для поискового запроса изображений
   /// </summary>
   public interface IImageRequest
   {
      /// <summary>
      /// Критерий поиска
      /// </summary>
      string SearchTerm { get; }

      /// <summary>
      /// Url-запроса
      /// </summary>
      string Url { get; }

      /// <summary>
      /// Разбор результатов
      /// </summary>
      /// <param name="xml">Разметка</param>
      /// <returns>Набор результатов поиска</returns>
      IEnumerable<SearchItemResult> Parse(string xml);

      /// <summary>
      /// Параметры аутентификации
      /// </summary>
      ICredentials Credentials { get; }
   }
}