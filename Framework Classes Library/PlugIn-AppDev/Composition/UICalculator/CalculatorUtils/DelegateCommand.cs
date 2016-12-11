using System;
using System.Windows.Input;
using CalculatorUtils.Annotations;

namespace CalculatorUtils
{
	public class DelegateCommand : ICommand
	{
		private readonly Func<bool> _canExecute;
		private readonly Action _execute;

		public DelegateCommand([NotNull] Action execute, [CanBeNull] Func<bool> canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

		public void Execute(object parameter) => _execute.Invoke();

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}