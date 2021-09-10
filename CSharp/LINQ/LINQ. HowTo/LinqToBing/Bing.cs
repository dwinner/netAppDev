using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LinqToBing
{
   class SearchResult
   {
      public string Title { get; set; }
      public string Url { get; set; }
      public string Description { get; set; }
   }

   class Bing
   {
      public string AppId { get; set; }
      const string BING_NAME_SPACE = "http://schemas.microsoft.com/LiveSearch/2008/04/XML/web";

      public Bing(string appId)
      {
         AppId = appId;
      }

      public IEnumerable<SearchResult> QueryWeb(string query)
      {
         string escaped = Uri.EscapeUriString(query);
         string url = BuildUrl(escaped);
         XDocument doc = XDocument.Load(url);
         XNamespace ns = BING_NAME_SPACE;
         IEnumerable<SearchResult> results = from sr in doc.Descendants(ns + "WebResult")
                                             let titleElement = sr.Element(ns + "Title")
                                             where titleElement != null
                                             let urlElement = sr.Element(ns + "Url")
                                             where urlElement != null
                                             let descriptionElement = sr.Element(ns + "Description")
                                             where descriptionElement != null
                                             select new SearchResult
                                             {
                                                Title = titleElement.Value,
                                                Url = urlElement.Value,
                                                Description = descriptionElement.Value
                                             };
         return results;
      }

      string BuildUrl(string query)
      {
         return "http://api.search.live.net/xml.aspx?"
             + "AppId=" + AppId
             + "&Query=" + query
             + "&Sources=Web"
             + "&Version=2.0"
             + "&Web.Count=10"
             + "&Web.Offset=0"
             ;
      }
   }
}
