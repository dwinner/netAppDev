using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Commands
{
   public partial class MonitorCommands : Window
   {
      private static readonly RoutedUICommand ApplicationUndoFld;

      static MonitorCommands()
      {
         ApplicationUndoFld = new RoutedUICommand(
            "ApplicationUndo", "Application Undo", typeof(MonitorCommands));
      }

      public MonitorCommands()
      {
         InitializeComponent();

         AddHandler(CommandManager.PreviewExecutedEvent,
            new ExecutedRoutedEventHandler(CommandExecuted));
      }

      public static RoutedUICommand ApplicationUndo
      {
         get { return ApplicationUndoFld; }
      }

      private void window_Unloaded(object sender, RoutedEventArgs e)
      {
         RemoveHandler(CommandManager.PreviewExecutedEvent,
            new ExecutedRoutedEventHandler(CommandExecuted));
      }

      private void CommandExecuted(object sender, ExecutedRoutedEventArgs e)
      {
         // Ignore menu button source.
         if (e.Source is ICommandSource)
            return;

         // Ignore the ApplicationUndo command.
         if (e.Command == ApplicationUndo)
            return;

         // Could filter for commands you want to add to the stack
         // (for example, not selection events).

         var txt = e.Source as TextBox;
         if (txt != null)
         {
            var cmd = (RoutedCommand)e.Command;

            var historyItem = new CommandHistoryItem(cmd.Name, txt, "Text", txt.Text);

            var item = new ListBoxItem { Content = historyItem };
            HistoryListBox.Items.Add(historyItem);

            // CommandManager.InvalidateRequerySuggested();
         }
      }

      private void ApplicationUndoCommand_Executed(object sender, RoutedEventArgs e)
      {
         var historyItem = (CommandHistoryItem)HistoryListBox.Items[HistoryListBox.Items.Count - 1];
         if (historyItem.CanUndo)
            historyItem.Undo();
         HistoryListBox.Items.Remove(historyItem);
      }

      private void ApplicationUndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
      {
         e.CanExecute = HistoryListBox != null && HistoryListBox.Items.Count != 0;
      }
   }

   public class CommandHistoryItem
   {
      public CommandHistoryItem(string commandName, UIElement elementActedOn = null,
         string propertyActedOn = "", object previousState = null)
      {
         CommandName = commandName;
         ElementActedOn = elementActedOn;
         PropertyActedOn = propertyActedOn;
         PreviousState = previousState;
      }

      public string CommandName { get; set; }

      public UIElement ElementActedOn { get; set; }

      public string PropertyActedOn { get; set; }

      public object PreviousState { get; set; }

      public bool CanUndo
      {
         get { return ElementActedOn != null && PropertyActedOn != ""; }
      }

      public void Undo()
      {
         var elementType = ElementActedOn.GetType();
         var property = elementType.GetProperty(PropertyActedOn);
         property.SetValue(ElementActedOn, PreviousState, null);
      }
   }
}