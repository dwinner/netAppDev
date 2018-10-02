using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Printing
{   
   public partial class PrintCustomPages
   {
      public PrintCustomPages()
      {
         InitializeComponent();
      }

      private void OnPrint(object sender, RoutedEventArgs e)
      {
         var printDialog = new PrintDialog();
         if (printDialog.ShowDialog() == true)
         {
            var dataSet = new DataSet();
            dataSet.ReadXmlSchema("store.xsd");
            dataSet.ReadXml("store.xml");

            DocumentPaginator paginator = new StoreDataSetPaginator(dataSet.Tables[0],
               new Typeface("Calibri"), 24, 96 * 0.75,
               new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));

            printDialog.PrintDocument(paginator, "Custom-Printed Pages");
         }
      }
   }
}