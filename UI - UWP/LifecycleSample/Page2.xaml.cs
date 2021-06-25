using LifecycleSample.DataModel;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace LifecycleSample
{
   public sealed partial class Page2
   {
      private Page2Data _data;

      public Page2()
      {
         InitializeComponent();
      }

      protected override void LoadState(Dictionary<string, object> pageState)
      {
         _data = pageState != null && pageState.ContainsKey("Page2")
            ? pageState["Page2"] as Page2Data
            : new Page2Data { Data = "initital _data" };

         DefaultViewModel["Page2Data"] = _data;
      }

      protected override void SaveState(Dictionary<string, object> pageState)
      {
         pageState.Add("Page2", _data);
      }

      private void OnGotoPage3(object sender, RoutedEventArgs e)
      {
         Frame.Navigate(typeof(Page3));
      }
   }
}