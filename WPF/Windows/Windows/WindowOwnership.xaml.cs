using System.Windows;

namespace Windows
{
   public partial class WindowOwnership
   {
      public WindowOwnership()
      {
         InitializeComponent();
      }

      private void OnCreateOwnedWindow(object sender, RoutedEventArgs e)
      {
         var win = new WindowOwnership
         {
            Owner = this,
            Title = "Owned Window",
            Height = Height / 2,
            Width = Width / 2,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
         };

         win.Show();
      }
   }
}