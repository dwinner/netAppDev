using System.Windows;
using System.Windows.Controls;

namespace Animation
{
   public partial class RotateButtonWithLayout
   {
      public RotateButtonWithLayout()
      {
         InitializeComponent();
      }

      private void OnClick(object sender, RoutedEventArgs e)
      {
         LblTextBlock.Text = string.Format("You clicked: {0}", ((Button) e.OriginalSource).Content);
      }
   }
}