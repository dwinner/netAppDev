using System.Windows;
using System.Windows.Controls;

namespace Animation
{
   public partial class RotateButton
   {
      public RotateButton()
      {
         InitializeComponent();
      }

      private void OnClicked(object sender, RoutedEventArgs e)
      {
         LblTextBlock.Text = string.Format("You clicked: {0}", ((Button) e.OriginalSource).Content);
      }
   }
}