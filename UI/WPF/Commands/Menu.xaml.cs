using System.Windows;
using System.Windows.Controls;

namespace Commands
{   
   public partial class Menu
   {
      public Menu()
      {
         InitializeComponent();
      }

      private void ButtonClick(object sender, RoutedEventArgs e)
      {
         // Get the current button.
         var cmd = (Button) e.OriginalSource;

         // Create an instance of the window named
         // by the current button.
         var type = GetType();
         var assembly = type.Assembly;
         var win = (Window) assembly.CreateInstance(string.Format("{0}.{1}", type.Namespace, cmd.Content));

         // Show the window.
         if (win != null)
            win.ShowDialog();
      }
   }
}