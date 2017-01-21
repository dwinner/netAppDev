using System.Windows;

namespace Windows
{
   public partial class CenterScreen
   {
      public CenterScreen()
      {
         InitializeComponent();
      }

      private void OnSetByCenterScreen(object sender, RoutedEventArgs e)
      {
         var height = SystemParameters.WorkArea.Height;
         var width = SystemParameters.WorkArea.Width;
         Top = (height - Height) / 2;
         Left = (width - Width) / 2;
      }
   }
}