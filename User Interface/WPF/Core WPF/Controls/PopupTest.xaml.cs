using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Controls
{
   public partial class PopupTest
   {
      public PopupTest()
      {
         InitializeComponent();
      }

      private void run_MouseEnter(object sender, MouseEventArgs e)
      {
         PopLink.IsOpen = true;
      }

      private void lnk_Click(object sender, RoutedEventArgs e)
      {
         Process.Start(((Hyperlink) sender).NavigateUri.ToString());
      }
   }
}