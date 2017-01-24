using System;
using System.Windows.Navigation;

namespace NavigationApplication
{
   public partial class Page3
   {
      public Page3()
      {
         InitializeComponent();
      }

      private void Init(object sender, EventArgs e)
      {
         var navigationService = NavigationService.GetNavigationService(this);
         if (navigationService != null)
            while (navigationService.CanGoBack)
               navigationService.RemoveBackEntry();
      }
   }
}