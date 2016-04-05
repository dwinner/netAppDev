using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace AsyncLib
{
   /// <summary>
   /// Класс, инкапсулирующий запросы к поисковой службе Bing
   /// </summary>
   public class BingRequest : IImageRequest
   {
      private const int DefaultImageCount = 50;

      private const int DefaultImageOffset = 0;

      private const string DataServiceUrl =
         "https://api.datamarket.azure.com/Data.ashx/Bing/Search/v1/Image?Query=%27{0}%27&$top={1}&$skip={2}&$format=Atom";

      static BingRequest() // Note: Такие данные лучше внедрять в виде параметров
      {
         var settingsReader = new AppSettingsReader();
         BingApplicationId = (string)settingsReader.GetValue("BingAppId", typeof(string));
      }

      private static readonly string BingApplicationId;

      /// <summary>
      /// Кол-во запрошенных изображений
      /// </summary>
      public int Count { get; set; }

      /// <summary>
      /// Кол-во изображений, которые должны быть пропущены
      /// </summary>
      public int Offset { get; set; }

      /// <summary>
      /// Критерий поиска
      /// </summary>
      public string SearchTerm { get; set; }

      public BingRequest()
      {
         Count = DefaultImageCount;
         Offset = DefaultImageOffset;
      }

      /// <summary>
      /// Url для запрашивания изображений
      /// </summary>
      public string Url
      {
         get
         {
            return string.Format(DataServiceUrl, SearchTerm, Count, Offset);
         }
      }

      /// <summary>
      /// Разбор результатов поиска в набор элементов поиска
      /// </summary>
      /// <param name="xml">Разметка для разбора</param>
      /// <returns>Набор результатов поиска</returns>
      public IEnumerable<SearchItemResult> Parse(string xml)
      {
         XElement respXml = XElement.Parse(xml);
         XNamespace d = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices");
         XNamespace m = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");

         return (from item in respXml.Descendants(m + "properties")
                 let titleElement = item.Element(d + "Title")
                 where titleElement != null
                 let mediaUrlElement = item.Element(d + "MediaUrl")
                 where mediaUrlElement != null
                 let thumbnailElement = item.Element(d + "Thumbnail")
                 where thumbnailElement != null
                 let thumbMedia = thumbnailElement.Element(d + "MediaUrl")
                 where thumbMedia != null
                 select new SearchItemResult
                            {
                               Title = new string(titleElement.Value.Take(50).ToArray()),
                               Url = mediaUrlElement.Value,
                               ThumbnailUrl = thumbMedia.Value,
                               Source = "Bing"
                            }).ToList();
      }

      /// <summary>
      /// Параметры аутентификации для службы Bing
      /// </summary>
      public ICredentials Credentials
      {
         get
         {
            return new NetworkCredential(BingApplicationId, BingApplicationId);
         }
      }
   }
}