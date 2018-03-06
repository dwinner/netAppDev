using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Commands
{
	public class CommandSlider : Slider, ICommandSource
	{
		//ICommand Interface Memembers
		//make Command a dependency property so it can be DataBound
		public static readonly DependencyProperty CommandProperty
			= DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CommandSlider),
				new PropertyMetadata(null, CommandChanged));

		//make CommandTarget a dependency property so it can be DataBound
		public static readonly DependencyProperty CommandTargetProperty
			= DependencyProperty.Register(nameof(CommandTarget), typeof(IInputElement), typeof(CommandSlider),
				new PropertyMetadata((IInputElement) null));

		//make CommandParameter a dependency property so it can be DataBound
		public static readonly DependencyProperty CommandParameterProperty
			= DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(CommandSlider),
				new PropertyMetadata((object) null));

		//keep a copy of the handler so it doesn't get garbage collected
		private static EventHandler _canExecuteChangedHandler;

		public ICommand Command
		{
			get { return (ICommand) GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public IInputElement CommandTarget
		{
			get { return (IInputElement) GetValue(CommandTargetProperty); }
			set { SetValue(CommandTargetProperty, value); }
		}

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		// Command dependency property change callback
		private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var cs = (CommandSlider) d;
			cs.HookUpCommand((ICommand) e.OldValue, (ICommand) e.NewValue);
		}

		// Add a new command to the Command Property
		private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
		{
			//if oldCommand is not null, then we need to remove the handlers
			if (oldCommand != null)
				RemoveCommand(oldCommand, newCommand);
			AddCommand(oldCommand, newCommand);
		}

		// Remove an old command from the Command Property
		private void RemoveCommand(ICommand oldCommand, ICommand newCommand)
		{
			EventHandler handler = CanExecuteChanged;
			oldCommand.CanExecuteChanged -= handler;
		}

		// add the command
		private void AddCommand(ICommand oldCommand, ICommand newCommand)
		{
			EventHandler handler = CanExecuteChanged;
			_canExecuteChangedHandler = handler;
			if (newCommand != null)
				newCommand.CanExecuteChanged += _canExecuteChangedHandler;
		}

		private void CanExecuteChanged(object sender, EventArgs e)
		{
			if (Command != null)
			{
				var command = Command as RoutedCommand;
				IsEnabled = command?.CanExecute(CommandParameter, CommandTarget)
					?? Command.CanExecute(CommandParameter);
			}
		}

		// if Command is defined, then moving the slider will invoke the command;
		// otherwise, the silder will behave normally
		protected override void OnValueChanged(double oldValue, double newValue)
		{
			base.OnValueChanged(oldValue, newValue);

			if (Command == null)
				return;

			var command = Command as RoutedCommand;
			if (command != null)
				command.Execute(CommandParameter, CommandTarget);
			else
				Command.Execute(CommandParameter);
		}
	}
}