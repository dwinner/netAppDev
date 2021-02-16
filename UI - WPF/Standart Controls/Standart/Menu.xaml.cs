using System.Windows;
using System.Windows.Controls;

namespace Controls
{
   public partial class Menu
   {
      public Menu()
      {
         InitializeComponent();
      }

      private void ButtonClick(object sender, RoutedEventArgs e)
      {
         var source = (Button)e.OriginalSource;
         var type = GetType();
         var assembly = type.Assembly;
         var win = (Window)assembly.CreateInstance(string.Format("{0}.{1}", type.Namespace, source.Content));
         if (win != null)
            win.ShowDialog();
      }
   }
}