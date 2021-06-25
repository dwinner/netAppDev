using System.Collections.Generic;
using Windows.UI.Xaml;

namespace LifecycleSample
{
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      protected override void LoadState(Dictionary<string, object> pageState)
      {
      }

      protected override void SaveState(Dictionary<string, object> pageState)
      {
      }

      private void OnGotoPage2(object sender, RoutedEventArgs e)
      {
         Frame.Navigate(typeof(Page2));
      }
   }
}