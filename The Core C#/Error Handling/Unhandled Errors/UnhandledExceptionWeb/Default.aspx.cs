/**
 * Перехват необработанных исключений в Web
 */
using System;

namespace UnhandledExceptionWeb
{
   public class Default : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {                 
         // Обработка исключения на уровне страницы
         Error += DefaultError;
      }

      void DefaultError(object sender, EventArgs e)
      {
         Response.Redirect("ErrorHandlerPage.aspx");
      }
   }
}

namespace HowToCSharp.Ch04.UnhandledExceptionWeb
{
   public partial class Default : global::UnhandledExceptionWeb.Default
   {
      protected void ButtonIdClick(object sender, EventArgs e)
      {         
         throw new ArgumentNullException("Oops!");
      }      
   }
}
