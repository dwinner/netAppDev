using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Commands
{
	public partial class MonitorCommands
	{
		static MonitorCommands()
		{
			ApplicationUndo = new RoutedUICommand(
				"ApplicationUndo", "Application Undo", typeof(MonitorCommands));
		}

		public MonitorCommands()
		{
			InitializeComponent();

			AddHandler(CommandManager.PreviewExecutedEvent,
				new ExecutedRoutedEventHandler(CommandExecuted));
		}

		public static RoutedUICommand ApplicationUndo { get; }

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
				var cmd = (RoutedCommand) e.Command;

				var historyItem = new CommandHistoryItem(cmd.Name, txt, "Text", txt.Text);

				var item = new ListBoxItem {Content = historyItem};
				HistoryListBox.Items.Add(historyItem);

				// CommandManager.InvalidateRequerySuggested();
			}
		}

		private void ApplicationUndoCommand_Executed(object sender, RoutedEventArgs e)
		{
			var historyItem = (CommandHistoryItem) HistoryListBox.Items[HistoryListBox.Items.Count - 1];
			if (historyItem.CanUndo)
				historyItem.Undo();
			HistoryListBox.Items.Remove(historyItem);
		}

		private void ApplicationUndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = HistoryListBox != null && HistoryListBox.Items.Count != 0;
		}
	}
}