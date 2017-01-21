using System.Windows;

namespace Windows
{
   public partial class SavePosition
   {
      public SavePosition()
      {
         InitializeComponent();
      }

      private void OnSave(object sender, RoutedEventArgs e)
      {
         WindowPositionHelper.SaveSize(this);
      }

      private void OnRestore(object sender, RoutedEventArgs e)
      {
         WindowPositionHelper.SetSize(this);
      }
   }
}