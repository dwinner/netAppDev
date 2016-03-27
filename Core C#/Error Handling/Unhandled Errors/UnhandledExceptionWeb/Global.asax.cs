using System;

namespace HowToCSharp.Ch04.UnhandledExceptionWeb
{
   public class Global : System.Web.HttpApplication
   {
      protected void Application_Start(object sender, EventArgs e)
      {

      }

      protected void Session_Start(object sender, EventArgs e)
      {

      }

      protected void Application_BeginRequest(object sender, EventArgs e)
      {

      }

      protected void Application_AuthenticateRequest(object sender, EventArgs e)
      {

      }

      protected void Application_Error(object sender, EventArgs e)
      {
         Server.Transfer("ErrorHandlerPage.aspx");
         // Можно избежать записи в журнал событий
         var lastError = Server.GetLastError();
         Server.ClearError();
      }

      protected void Session_End(object sender, EventArgs e)
      {

      }

      protected void Application_End(object sender, EventArgs e)
      {

      }
   }
}