using System.Web.Mvc;
using System.Web.Routing;

namespace Gallery.Web.App_Start
{
   /// <summary>
   /// Конфигурация маршрутов
   /// </summary>
   public static class RouteConfig
   {
      /// <summary>
      /// Регистрация маршрутов
      /// </summary>
      /// <param name="routes">Коллекция маршрутов</param>
      public static void RegisterRoutes(RouteCollection routes)
      {
         routes.IgnoreRoute("{resource}.axd/{*pathInfo}");         
         routes.MapRoute(null, "", new { controller = "Picture", action = "List", page = 1 } );
         routes.MapRoute(null, "Page{page}", new { controller = "Picture", action = "List" }, new { page = @"\d+" } );
         routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
      }
   }
}