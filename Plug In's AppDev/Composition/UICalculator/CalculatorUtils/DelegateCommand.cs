using System;
using System.Windows.Input;

namespace CalculatorUtils
{
	public class DelegateCommand : ICommand
	{
		private readonly Func<bool> _canExecute;
		private readonly Action _execute;

		public DelegateCommand(Action execute, Func<bool> canExecute = null)
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));

			_execute = execute;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

		public void Execute(object parameter) => _execute();

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}