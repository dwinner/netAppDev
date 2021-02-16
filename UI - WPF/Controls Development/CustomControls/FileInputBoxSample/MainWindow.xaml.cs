using System.Diagnostics;
using System.Windows;

namespace FileInputBoxSample
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnFileNameChanged(object sender, RoutedEventArgs e)
      {
#if DEBUG
         Debug.WriteLine("Raised");
#endif
      }
   }
}