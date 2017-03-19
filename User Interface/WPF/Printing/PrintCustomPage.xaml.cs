using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Printing
{
   public partial class PrintCustomPage
   {
      public PrintCustomPage()
      {
         InitializeComponent();
      }

      private void OnPrint(object sender, RoutedEventArgs e)
      {
         var printDialog = new PrintDialog();
         if (printDialog.ShowDialog() == true)
         {
            // Create a visual for the page.
            var visual = new DrawingVisual();

            // Get the drawing context
            using (var drawingContext = visual.RenderOpen())
            {
               // Define the text you want to print.
               var text = new FormattedText(ContentTextBox.Text,
                  CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                  new Typeface("Calibri"), 20, Brushes.Black)
               {
                  MaxTextWidth = printDialog.PrintableAreaWidth / 2  // You must pick a maximum width to use wrapping with a FormattedText object.
               };               

               // Get the size required for the text.
               var textSize = new Size(text.Width, text.Height);

               // Find the top-left corner where you want to place the text.
               const double margin = 96 * 0.25;
               var point = new Point(
                  (printDialog.PrintableAreaWidth - textSize.Width) / 2 - margin,
                  (printDialog.PrintableAreaHeight - textSize.Height) / 2 - margin);

               // Draw the content.
               drawingContext.DrawText(text, point);

               // Add a border (a rectangle with no background).
               drawingContext.DrawRectangle(null, new Pen(Brushes.Black, 1),
                  new Rect(margin, margin,
                     printDialog.PrintableAreaWidth - margin * 2,
                     printDialog.PrintableAreaHeight - margin * 2));
            }

            // Print the visual.
            printDialog.PrintVisual(visual, "A Custom-Printed Page");
         }
      }
   }
}