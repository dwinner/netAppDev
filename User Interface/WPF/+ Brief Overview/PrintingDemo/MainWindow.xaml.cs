using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace PrintingDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnPrint(object sender, RoutedEventArgs e)
      {
         var dlg = new PrintDialog();
         if (dlg.ShowDialog() == true)
            dlg.PrintVisual(PrintCanvas, "Print Demo");

         // PrintServer printServer = new PrintServer(@"\\treslunas\laserjet");
         var printServer = new LocalPrintServer();
         var queue = printServer.DefaultPrintQueue;
         var ticket = queue.DefaultPrintTicket;
         var capabilities = queue.GetPrintCapabilities(ticket);
         if (capabilities.DuplexingCapability.Contains(Duplexing.TwoSidedLongEdge))
            ticket.Duplexing = Duplexing.TwoSidedLongEdge;
         if (capabilities.InputBinCapability.Contains(InputBin.AutoSelect))
            ticket.InputBin = InputBin.AutoSelect;
         if (capabilities.MaxCopyCount > 3)
            ticket.CopyCount = 3;
         if (capabilities.PageOrientationCapability.Contains(PageOrientation.Landscape))
            ticket.PageOrientation = PageOrientation.Landscape;
         if (capabilities.PagesPerSheetCapability.Contains(2))
            ticket.PagesPerSheet = 2;
         if (capabilities.StaplingCapability.Contains(Stapling.StapleBottomLeft))
            ticket.Stapling = Stapling.StapleBottomLeft;

         var writer = PrintQueue.CreateXpsDocumentWriter(queue);
         writer.Write(PrintCanvas, ticket);
      }
   }
}