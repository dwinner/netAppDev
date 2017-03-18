using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Printing
{
   public partial class PrintFlowDocument
   {
      public PrintFlowDocument()
      {
         InitializeComponent();
      }

      private void OnPrint(object sender, RoutedEventArgs e)
      {
         var printDialog = new PrintDialog();
         if (printDialog.ShowDialog() == true)
            printDialog.PrintDocument(((IDocumentPaginatorSource) DocReader.Document).DocumentPaginator,
               "A Flow Document");
      }

      private void OnCustomPrint(object sender, RoutedEventArgs e)
      {
         var printDialog = new PrintDialog();
         if (printDialog.ShowDialog() == true)
         {
            var doc = DocReader.Document;

            // Save all the existing settings.                                
            var oldPageHeight = doc.PageHeight;
            var oldPageWidth = doc.PageWidth;
            var oldPagePadding = doc.PagePadding;
            var oldColumnGap = doc.ColumnGap;
            var oldColumnWidth = doc.ColumnWidth;

            // Make the FlowDocument page match the printed page.
            doc.PageHeight = printDialog.PrintableAreaHeight;
            doc.PageWidth = printDialog.PrintableAreaWidth;
            doc.PagePadding = new Thickness(50);

            // Use two columns.
            doc.ColumnGap = 25;
            doc.ColumnWidth = (doc.PageWidth - doc.ColumnGap
                               - doc.PagePadding.Left - doc.PagePadding.Right) / 2;

            printDialog.PrintDocument(((IDocumentPaginatorSource) doc).DocumentPaginator, "A Flow Document");

            // Reapply the old settings.
            doc.PageHeight = oldPageHeight;
            doc.PageWidth = oldPageWidth;
            doc.PagePadding = oldPagePadding;
            doc.ColumnGap = oldColumnGap;
            doc.ColumnWidth = oldColumnWidth;
         }
      }
   }
}