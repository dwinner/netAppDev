using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Printing
{
   public class HeaderedFlowDocumentPaginator : DocumentPaginator
   {
      private readonly DocumentPaginator _flowDocumentPaginator;

      public HeaderedFlowDocumentPaginator(IDocumentPaginatorSource document)
      {
         _flowDocumentPaginator = document.DocumentPaginator;
      }

      /// <inheritdoc />
      public override bool IsPageCountValid
      {
         get { return _flowDocumentPaginator.IsPageCountValid; }
      }

      /// <inheritdoc />
      public override int PageCount
      {
         get { return _flowDocumentPaginator.PageCount; }
      }

      /// <inheritdoc />
      public override Size PageSize
      {
         get { return _flowDocumentPaginator.PageSize; }
         set { _flowDocumentPaginator.PageSize = value; }
      }

      /// <inheritdoc />
      public override IDocumentPaginatorSource Source
      {
         get { return _flowDocumentPaginator.Source; }
      }

      /// <inheritdoc />
      public override DocumentPage GetPage(int pageNumber)
      {
         // Get the requested page.
         var page = _flowDocumentPaginator.GetPage(pageNumber);

         // Wrap the page in a Visual. You can then add a transformation and extras.
         var newVisual = new ContainerVisual();
         newVisual.Children.Add(page.Visual);
         newVisual.Children.Add(CreateHeader(pageNumber)); // Add the title to the visual.         
         var newPage = new DocumentPage(newVisual); // Wrap the visual in a new page.

         return newPage;
      }

      private static DrawingVisual CreateHeader(int pageNumber)
      {
         // Create a header. 
         var header = new DrawingVisual();
         using (var context = header.RenderOpen())
         {
            var typeface = new Typeface("Times New Roman");
            var text = new FormattedText(string.Format("Page {0}", pageNumber + 1), CultureInfo.CurrentCulture,
               FlowDirection.LeftToRight, typeface, 14, Brushes.Black);

            // Leave a quarter-inch of space between the page edge and this text.
            context.DrawText(text, new Point(96 * 0.25, 96 * 0.25));
         }

         return header;
      }
   }
}