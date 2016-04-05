using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace IdentityAndSecurity
{
   public partial class Startup
   {
      // Дополнительные сведения о настройке проверки подлинности см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301864
      public static void ConfigureAuth(IAppBuilder app)
      {
         // Включение использования файла cookie, в котором приложение может хранить информацию для пользователя, выполнившего вход,
         app.UseCookieAuthentication(new CookieAuthenticationOptions
         {
            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            LoginPath = new PathString("/Account/Login")
         });
         // Use a cookie to temporarily store information about a user logging in with a third party login provider
         app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

         //app.UseMicrosoftAccountAuthentication("000000004013EC43", "wdOgXX5bIo5iS9Z-aqEtorWSt88yhogZ");
         //app.UseTwitterAuthentication("T8vvpszu324cdgnRSQ4Lczvbv", "R6sHMxYUDgAqaiiKVTWukBAfiNIPEmcG5tv99nwyhTj0LdblZQ");
         //app.UseFacebookAuthentication("1523955557819484", "f724df6950fdacf59c01788f510f7055");
         app.UseGoogleAuthentication();
      }
   }
}