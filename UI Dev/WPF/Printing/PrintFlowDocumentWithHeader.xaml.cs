using System.Windows;
using System.Windows.Controls;

namespace Printing
{   
   public partial class PrintFlowDocumentWithHeader
   {
      public PrintFlowDocumentWithHeader()
      {
         InitializeComponent();
      }

      private void OnPrint(object sender, RoutedEventArgs e)
      {
         var printDialog = new PrintDialog();
         if (printDialog.ShowDialog() == true)
         {
            var flowDocument = DocReader.Document;

            // Save all the existing settings.                                            
            var pageHeight = flowDocument.PageHeight;
            var pageWidth = flowDocument.PageWidth;
            var pagePadding = flowDocument.PagePadding;
            var columnGap = flowDocument.ColumnGap;
            var columnWidth = flowDocument.ColumnWidth;

            // Make the FlowDocument page match the printed page.
            flowDocument.PageHeight = printDialog.PrintableAreaHeight;
            flowDocument.PageWidth = printDialog.PrintableAreaWidth;
            flowDocument.PagePadding = new Thickness(50);

            // Use two columns.
            flowDocument.ColumnGap = 25;
            flowDocument.ColumnWidth = (flowDocument.PageWidth - flowDocument.ColumnGap
                                              - flowDocument.PagePadding.Left -
                                              flowDocument.PagePadding.Right) / 2;

            var document = flowDocument;
            DocReader.Document = null;

            var paginator = new HeaderedFlowDocumentPaginator(document);
            printDialog.PrintDocument(paginator, "A Flow Document");

            DocReader.Document = document;

            // Reapply the old settings.
            flowDocument.PageHeight = pageHeight;
            flowDocument.PageWidth = pageWidth;
            flowDocument.PagePadding = pagePadding;
            flowDocument.ColumnGap = columnGap;
            flowDocument.ColumnWidth = columnWidth;
         }
      }
   }
}