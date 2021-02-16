using System.Windows;
using System.Windows.Input;

namespace WpfTextEditor
{
   public partial class SimpleTextEditor
   {
      public SimpleTextEditor()
      {
         InitializeComponent();

         //setup handlers for our custom commands
         var exitCommandBinding = new CommandBinding(WpfTextEditorCommands.ExitCommand);
         exitCommandBinding.Executed += OnExitExecuted;
         exitCommandBinding.CanExecute += OnExitCanExecute;

         var wordWrapCommandBinding = new CommandBinding(WpfTextEditorCommands.WordWrapCommand);
         wordWrapCommandBinding.Executed += OnWordWrapExecuted;

         CommandBindings.Add(exitCommandBinding);
         CommandBindings.Add(wordWrapCommandBinding);
      }

      private void OnExitCanExecute(object sender, CanExecuteRoutedEventArgs e)
      {
         e.CanExecute = EditorTextBox.Text.Length == 0;
      }

      private void OnWordWrapExecuted(object sender, ExecutedRoutedEventArgs e)
      {
         EditorTextBox.TextWrapping = EditorTextBox.TextWrapping == TextWrapping.NoWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
      }

      private static void OnExitExecuted(object sender, ExecutedRoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }
   }
}