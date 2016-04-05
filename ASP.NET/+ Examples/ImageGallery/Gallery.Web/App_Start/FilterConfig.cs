using System.Web.Mvc;

namespace Gallery.Web.App_Start
{
   /// <summary>
   /// Конфигурация глобальных фильтров контроллеров
   /// </summary>
   public static class FilterConfig
   {
      /// <summary>
      /// Регистрация фильтров в контексте всего приложения
      /// </summary>
      /// <param name="filters">Коллекция глобальных фильтров</param>
      public static void RegisterGlobalFilters(GlobalFilterCollection filters)
      {
         filters.Add(new HandleErrorAttribute());
      }
   }
}