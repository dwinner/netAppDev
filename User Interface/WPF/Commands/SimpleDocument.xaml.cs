using System.Windows;
using System.Windows.Input;

namespace Commands
{
   public partial class SimpleDocument
   {
      private bool _isDirty;

      public SimpleDocument()
      {
         InitializeComponent();

         // Create bindings.
         var binding = new CommandBinding(ApplicationCommands.New);
         binding.Executed += NewCommand;
         CommandBindings.Add(binding);

         binding = new CommandBinding(ApplicationCommands.Open);
         binding.Executed += OpenCommand;
         CommandBindings.Add(binding);

         binding = new CommandBinding(ApplicationCommands.Save);
         binding.Executed += SaveCommand_Executed;
         binding.CanExecute += SaveCommand_CanExecute;
         CommandBindings.Add(binding);
      }

      private void NewCommand(object sender, ExecutedRoutedEventArgs e)
      {
         MessageBox.Show(string.Format("New command triggered with {0}", e.Source));
         _isDirty = false;
      }

      private void OpenCommand(object sender, ExecutedRoutedEventArgs e)
      {
         _isDirty = false;
      }

      private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         MessageBox.Show(string.Format("Save command triggered with {0}", e.Source));
         _isDirty = false;
      }

      private void txt_TextChanged(object sender, RoutedEventArgs e)
      {
         _isDirty = true;
      }

      private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
      {
         e.CanExecute = _isDirty;
      }
   }
}