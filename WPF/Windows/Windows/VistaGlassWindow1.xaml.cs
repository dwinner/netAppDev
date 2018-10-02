using System.Windows;
using System.Windows.Media;

namespace Windows
{
   public partial class VistaGlassWindow1
   {
      public VistaGlassWindow1()
      {
         InitializeComponent();
      }

      private void OnLoaded(object sender, RoutedEventArgs e)
      {
         try
         {
            VistaGlassHelper.ExtendGlass(this, -1, -1, -1);
         }
         // If not Vista, paint background white.
         catch //(DllNotFoundException)
         {
            Background = Brushes.White;
         }
      }
   }
}