using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WCFRegistration.Views
{
   public partial class Welcome : Page
   {
      public Welcome()
      {
         InitializeComponent();
      }

      // Executes when the user navigates to this page.
      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         startStoryboard.Completed += (sender1, e1) =>
             {
                endStoryboard.Completed += (sender2, e2) =>
                    NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
                endStoryboard.Begin();
             };

         startStoryboard.Begin();
      }

   }
}
