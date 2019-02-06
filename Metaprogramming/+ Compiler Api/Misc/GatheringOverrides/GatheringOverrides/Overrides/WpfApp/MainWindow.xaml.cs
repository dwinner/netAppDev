using System.Windows.Media;

namespace WpfApp
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      protected virtual int Own { get; set; }      
   }
}