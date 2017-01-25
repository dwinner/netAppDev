using System.Runtime.InteropServices;
using System.Windows;
using System.IO;

namespace WebBrowserTest
{   
   public partial class CallWpfWithJavaScript : Window
   {
      public CallWpfWithJavaScript()
      {
         InitializeComponent();
         WebBrowser.Navigate(string.Format("file:///{0}",
            Path.Combine(Path.GetDirectoryName(Application.ResourceAssembly.Location), "sample.htm")));
         WebBrowser.ObjectForScripting = new HtmlBridge();
      }
   }

   [ComVisible(true)]
   public class HtmlBridge
   {
      public void WebClick(string source)
      {
         MessageBox.Show(string.Format("Received: {0}", source));
      }
   }

}
