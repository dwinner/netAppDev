using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Printing
{
   public partial class PrintScaledVisual
   {
      public PrintScaledVisual()
      {
         InitializeComponent();
      }

      private void OnPrint(object sender, RoutedEventArgs e)
      {
         var printDialog = new PrintDialog();
         if (printDialog.ShowDialog() == true)
         {
            // Create the text.
            var run = new Run("This is a test of the printing functionality in the Windows Presentation Foundation.");

            // Wrap it in a TextBlock.
            var visual = new TextBlock(run)
            {
               Margin = new Thickness(15),
               TextWrapping = TextWrapping.Wrap
            };
            // Allow wrapping to fit the page width.

            // Scale the TextBlock in both dimensions.
            double zoom;
            if (double.TryParse(ScaleTextBox.Text, out zoom))
            {
               visual.LayoutTransform = new ScaleTransform(zoom / 100, zoom / 100);

               // Get the size of the page.
               var pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

               // Trigger the sizing of the element.                
               visual.Measure(pageSize);
               visual.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));

               // Print the element.
               printDialog.PrintVisual(visual, "A Scaled Drawing");
            }
            else
            {
               MessageBox.Show("Invalid scale value.");
            }
         }
      }
   }
}