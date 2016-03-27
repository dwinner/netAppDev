using System.IO;
using System.Web.Mvc;

namespace Gallery.Web.Filters
{
   /// <summary>
   /// Фильтр для действий при хранении изображений в файловой системе
   /// </summary>
   public class PictureLocalStorageAttribute : ActionFilterAttribute
   {
      public override void OnActionExecuting(ActionExecutingContext filterContext)
      {
         var httpContext = filterContext.HttpContext;
         string profileUserName = httpContext.Profile.UserName;
         string userNameLocalStorage = string.Format("{0}\\App_Data\\{1}", httpContext.Request.PhysicalApplicationPath,
            profileUserName);
         if (!Directory.Exists(userNameLocalStorage))
         {
            Directory.CreateDirectory(userNameLocalStorage);
         }

         base.OnActionExecuting(filterContext);
      }
   }
}