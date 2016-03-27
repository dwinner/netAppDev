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

   public class DataCommands
   {
      static DataCommands()
      {
         var inputs = new InputGestureCollection { new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R") };
         Requery = new RoutedUICommand(
            "Requery", "Requery", typeof(DataCommands), inputs);
      }

      public static RoutedUICommand Requery { get; private set; }
   }
}