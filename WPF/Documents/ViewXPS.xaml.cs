using System.IO;
using System.Windows;
using System.Windows.Xps.Packaging;

namespace Documents
{   
   public partial class ViewXps
   {
      public ViewXps()
      {
         InitializeComponent();
      }

      private void OnWindowLoaded(object sender, RoutedEventArgs e)
      {
         using (var doc = new XpsDocument("ch19.xps", FileAccess.Read))
         {
            DocViewer.Document = doc.GetFixedDocumentSequence();
         }
      }
   }
}