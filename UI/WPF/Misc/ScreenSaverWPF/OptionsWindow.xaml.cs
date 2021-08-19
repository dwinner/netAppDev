using System.Windows;

namespace ScreenSaverWPF
{
   public partial class OptionsWindow
   {
      public OptionsWindow()
      {
         InitializeComponent();
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         Close();
      }
   }
}