using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace XBAP
{
   public partial class PopupTest
   {
      public PopupTest()
      {
         InitializeComponent();
      }

      private void OnStart(object sender, RoutedEventArgs e)
      {
         DisableMainPage();
      }

      private void OnOk(object sender, RoutedEventArgs e)
      {
         // Copy name from the Popup into the main page.
         LblName.Content = string.Format("You entered: {0}", TxtName.Text);
         EnableMainPage();
      }

      private void OnCancel(object sender, RoutedEventArgs e)
      {
         EnableMainPage();
      }

      private void EnableMainPage()
      {
         MainPage.IsEnabled = true;
         Background = null;
         DialogPopUp.IsOpen = false;
      }

      private void DisableMainPage()
      {
         MainPage.IsEnabled = false;
         Background = Brushes.LightGray;
         DialogPopUp.IsOpen = true;
      }

      private void OnStartWinForm(object sender, RoutedEventArgs e)
      {
         using (var form = new UserNameWinForm())
         {
            if (form.ShowDialog() == DialogResult.OK)
               LblNameWf.Content = form.UserName;
         }
      }
   }
}