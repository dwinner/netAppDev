using System.Windows;
using System.Windows.Media;

namespace Windows
{
   public partial class VistaGlassWindow2
   {
      public VistaGlassWindow2()
      {
         InitializeComponent();
      }

      private void OnLoaded(object sender, RoutedEventArgs e)
      {
         try
         {
            VistaGlassHelper.ExtendGlass(this, 5, 5, (int) TopBar.ActualHeight + 5);
         }
         // If not Vista, paint background white.
         catch //(DllNotFoundException)
         {
            Background = Brushes.White;
         }
      }
   }
}