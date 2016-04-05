using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using Gallery.Web.Annotations;
using WebMatrix.WebData;
using Gallery.Web.Models;

namespace Gallery.Web.Filters
{
   /// <summary>
   /// Фильтр для инициализации базы данных для учетных записей
   /// </summary>
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
   public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
   {
      private static SimpleMembershipInitializer _initializer;
      private static object _initializerLock = new object();
      private static bool _isInitialized;

      public override void OnActionExecuting(ActionExecutingContext filterContext)
      {         
         LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
      }

      [UsedImplicitly]
      private class SimpleMembershipInitializer
      {
         public SimpleMembershipInitializer()
         {
            Database.SetInitializer<UsersContext>(null);

            try
            {
               using (var context = new UsersContext())
               {
                  if (!context.Database.Exists())
                  {                     
                     ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                  }
               }

               WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
            }
            catch (Exception ex)
            {
               throw new InvalidOperationException("Fail initialize Membership database", ex);
            }
         }
      }
   }
}
