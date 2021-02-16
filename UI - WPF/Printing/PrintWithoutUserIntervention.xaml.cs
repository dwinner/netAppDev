using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Printing
{
   public partial class PrintWithoutUserIntervention
   {
      public PrintWithoutUserIntervention()
      {
         InitializeComponent();
      }

      private void OnPrint(object sender, RoutedEventArgs e)
      {
         var dialog = new PrintDialog {PrintQueue = LocalPrintServer.GetDefaultPrintQueue()};
         dialog.PrintVisual((Visual) sender, "Automatic Printout");
      }
   }
}