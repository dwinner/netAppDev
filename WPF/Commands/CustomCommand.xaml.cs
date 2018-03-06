using System.Windows;
using System.Windows.Input;

namespace Commands
{
   public partial class CustomCommand
   {
      public CustomCommand()
      {
         InitializeComponent();
      }

      private void RequeryCommand(object sender, ExecutedRoutedEventArgs e)
      {
         MessageBox.Show("Requery");
      }
   }
}