using System.Windows;

namespace CustomControlsClient
{
   public partial class FlipPanelTest
   {
      public FlipPanelTest()
      {
         InitializeComponent();
      }

      void OnFlip(object sender, RoutedEventArgs e)
      {
         panel.IsFlipped = !panel.IsFlipped;
      }
   }
}