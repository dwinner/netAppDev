using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityAndSecurity.Startup))]
namespace IdentityAndSecurity
{
   public partial class Startup
   {
      public void Configuration(IAppBuilder app)
      {
         ConfigureAuth(app);
      }
   }
}
