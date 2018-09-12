using System.Windows;

namespace SimpleWindow
{   
   public partial class Window1
   {
      public Window1()
      {
         InitializeComponent();
      }

      private void OnClick(object sender, RoutedEventArgs e)
      {
         var treeDisplay = new VisualTreeDisplay();
         treeDisplay.ShowVisualTree(this);
         treeDisplay.Show();
      }
   }
}