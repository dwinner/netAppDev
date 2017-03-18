using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Printing
{
   public partial class PrintVisual
   {
      public PrintVisual()
      {
         InitializeComponent();
      }

      private void OnPrint(object sender, RoutedEventArgs e)
      {
         #region commented

         //PrintDialog printDialog = new PrintDialog();
         //if (printDialog.ShowDialog() == true)
         //{
         //    printDialog.PrintVisual(canvas, "A Simple Drawing");
         //}

         #endregion

         var printDialog = new PrintDialog();
         if (printDialog.ShowDialog() == true)
         {
            // Scale the TextBlock in both dimensions.
            double zoom;
            if (double.TryParse(ScaleTextBox.Text, out zoom))
            {
               grid.Visibility = Visibility.Hidden;

               // Add a background to make it easier to see the canvas bounds.
               PrintCanvas.Background = Brushes.LightSteelBlue;

               // Resize it.
               PrintCanvas.LayoutTransform = new ScaleTransform(zoom / 100, zoom / 100);

               // Get the size of the page.
               var pageSize = new Size(printDialog.PrintableAreaWidth - 20, printDialog.PrintableAreaHeight - 20);

               // Trigger the sizing of the element.                                    
               PrintCanvas.Measure(pageSize);
               PrintCanvas.Arrange(new Rect(10, 10, pageSize.Width, pageSize.Height));

               // Print the element.
               printDialog.PrintVisual(PrintCanvas, "A Scaled Drawing");

               // Reset the canvas.
               PrintCanvas.Background = null;
               PrintCanvas.LayoutTransform = null;
               grid.Visibility = Visibility.Visible;
            }
            else
            {
               MessageBox.Show("Invalid scale value.");
            }
         }
      }
   }
}