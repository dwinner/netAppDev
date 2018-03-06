using System.Windows.Input;

namespace Commands
{
   public partial class NoCommandTextBox
   {
      public NoCommandTextBox()
      {
         InitializeComponent();

         Txt.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, null, SuppressCommand));

         Txt.InputBindings.Add(new KeyBinding(ApplicationCommands.NotACommand, Key.C, ModifierKeys.Control));
         //Txt.ContextMenu = null;
      }

      private void SuppressCommand(object sender, CanExecuteRoutedEventArgs e)
      {
         e.CanExecute = false;
         e.Handled = true;
      }
   }
}