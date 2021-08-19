using System.Windows.Input;

namespace Commands
{
	public class DataCommands
	{
		static DataCommands()
		{
			var inputs = new InputGestureCollection { new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R") };
			Requery = new RoutedUICommand(nameof(Requery), nameof(Requery), typeof(DataCommands), inputs);
		}

		public static RoutedUICommand Requery { get; }
	}
}