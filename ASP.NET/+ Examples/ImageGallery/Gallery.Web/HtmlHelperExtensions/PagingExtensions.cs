using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using Gallery.Web.Models;

namespace Gallery.Web.HtmlHelperExtensions
{
   /// <summary>
   /// Вспомогатеный класс для расширения возможностей визуализации Html
   /// </summary>
   public static class PagingExtensions
   {
      /// <summary>
      /// Разметка для страничного навигатора
      /// </summary>
      /// <param name="html">Вспомогательный объект визуализации Html</param>
      /// <param name="navigator">Постраничный навигатор</param>
      /// <param name="pageUrl">Делегат формирования Url для конкретной страницы</param>
      /// <returns>Кодированная строка Html</returns>
      public static MvcHtmlString PageLinks(this HtmlHelper html, PagingNavigator navigator, Func<int, string> pageUrl)
      {
         var result = new StringBuilder();
         for (int i = 1; i <= navigator.TotalPages; i++)
         {
            var tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(i));
            tag.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
            if (i == navigator.CurrentPage)
            {
               tag.AddCssClass("selected");
            }

            result.Append(tag);
         }

         return MvcHtmlString.Create(result.ToString());
      }
   }
}