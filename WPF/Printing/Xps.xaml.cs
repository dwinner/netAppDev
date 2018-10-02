using System;
using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps.Packaging;

namespace Printing
{
   public partial class Xps
   {
      private readonly PrintDialog _printDialog = new PrintDialog();

      public Xps()
      {
         InitializeComponent();
      }

      private void Window_Loaded(object sender, EventArgs e)
      {
         using (var doc = new XpsDocument("test.xps", FileAccess.ReadWrite))
         {
            DocViewer.Document = doc.GetFixedDocumentSequence();
         }
      }

      private void OnPrintXps(object sender, RoutedEventArgs e)
      {
         if (_printDialog.ShowDialog() == true)
            _printDialog.PrintDocument(DocViewer.Document.DocumentPaginator, "A Fixed Document");
      }

      private void OnPrintFlow(object sender, RoutedEventArgs e)
      {
         var filePath = Path.Combine(Directory.GetCurrentDirectory(), "FlowDocument1.xaml");
         if (_printDialog.ShowDialog() == true)
         {
            var queue = _printDialog.PrintQueue;
            var writer = PrintQueue.CreateXpsDocumentWriter(queue);

            using (var stream = File.Open(filePath, FileMode.Open))
            {
               var flowDocument = (FlowDocument) XamlReader.Load(stream);
               writer.Write(((IDocumentPaginatorSource) flowDocument).DocumentPaginator);
            }
         }
      }

      private void OnShowFlow(object sender, RoutedEventArgs e)
      {
         // Load the XPS content into memory.
         Package package;
         using (var memoryStream = new MemoryStream())
         {
            package = Package.Open(memoryStream, FileMode.Create, FileAccess.ReadWrite);
         }

         var documentUri = new Uri("pack://InMemoryDocument.xps");
         PackageStore.AddPackage(documentUri, package);
         using (var xpsDocument = new XpsDocument(package, CompressionOption.Fast, documentUri.AbsoluteUri))
         using (var fileStream = File.Open("FlowDocument1.xaml", FileMode.Open, FileAccess.ReadWrite))
         {
            var doc = (FlowDocument) XamlReader.Load(fileStream);
            var writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource) doc).DocumentPaginator);

            // Display the new XPS document in a viewer.
            DocViewer.Document = xpsDocument.GetFixedDocumentSequence();
         }
      }
   }
}