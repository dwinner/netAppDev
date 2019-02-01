using System.Net;
using System.Windows;
using System.Windows.Navigation;

namespace NavigationApplication
{
   public partial class App
   {
      private void App_NavigationFailed(object sender, NavigationFailedEventArgs e)
      {
         if (e.Exception is WebException)
         {
            MessageBox.Show(string.Format("Website {0} cannot be reached.", e.Uri));

            // Neutralize the error so the application continues running.
            e.Handled = true;
         }
      }
   }
}