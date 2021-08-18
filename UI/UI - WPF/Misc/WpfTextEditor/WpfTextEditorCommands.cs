using System.Windows.Input;

namespace WpfTextEditor
{
   public static class WpfTextEditorCommands
   {
      public static readonly RoutedUICommand ExitCommand;
      public static readonly RoutedUICommand WordWrapCommand;

      static WpfTextEditorCommands()
      {
         var exitInputs = new InputGestureCollection {new KeyGesture(Key.F4, ModifierKeys.Alt)};
         ExitCommand = new RoutedUICommand("Exit application", "ExitApplication",
            typeof(WpfTextEditorCommands), exitInputs);

         WordWrapCommand = new RoutedUICommand("Word wrap", "WordWrap",
            typeof(WpfTextEditorCommands));
      }
   }
}