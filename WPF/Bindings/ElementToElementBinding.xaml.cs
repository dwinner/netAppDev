using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataBinding
{
   public partial class ElementToElementBinding
   {
      public ElementToElementBinding()
      {
         InitializeComponent();
      }

      private void OnSetSmallClick(object sender, RoutedEventArgs e)
      {
         FontSizeSlider.Value = 2;
      }

      private void OnSetNormalClick(object sender, RoutedEventArgs e)
      {
         FontSizeSlider.Value = FontSize;
      }

      private void OnSetLargeClick(object sender, RoutedEventArgs e)
      {
         // Only works in two-way mode.
         SampleTextBlock.FontSize = 30;
      }

      private void OnGetBoundObject(object sender, RoutedEventArgs e)
      {
         var binding = BindingOperations.GetBinding(BoundTextBox, TextBox.TextProperty);
         if (binding != null)
            MessageBox.Show(string.Format("Bound {0} to source element {1}", binding.Path.Path, binding.ElementName));

         var expression = BindingOperations.GetBindingExpression(BoundTextBox, TextBox.TextProperty);
         if (expression != null)
            MessageBox.Show(string.Format("Bound {0} with data {1}", expression.ResolvedSourcePropertyName,
               ((TextBlock) expression.ResolvedSource).FontSize));
      }
   }
}