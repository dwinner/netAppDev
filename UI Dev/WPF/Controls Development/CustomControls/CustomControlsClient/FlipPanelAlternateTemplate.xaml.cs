using System.Windows;

namespace CustomControlsClient
{
   public partial class FlipPanelAlternateTemplate
   {
      public FlipPanelAlternateTemplate()
      {
         InitializeComponent();
      }

      void OnFlip(object sender, RoutedEventArgs e)
      {
         panel.IsFlipped = !panel.IsFlipped;
      }
   }
}