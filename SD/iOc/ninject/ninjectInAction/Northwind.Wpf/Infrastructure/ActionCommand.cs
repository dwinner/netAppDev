using System;
using System.Windows.Input;

namespace Northwind.Wpf.Infrastructure
{
   public class ActionCommand : ICommand
   {
      private readonly Action<object> _action;
      private readonly Func<object, bool> _canExecute = p => true;

      public ActionCommand(Action<object> action, Func<object, bool> canExecute = null)
      {
         _action = action ?? throw new ArgumentNullException(nameof(action));
         if (canExecute != null)
         {
            _canExecute = canExecute;
         }
      }

      public void Execute(object parameter) => _action(parameter);

      public bool CanExecute(object parameter) => _canExecute(parameter);

      public event EventHandler CanExecuteChanged;
   }
}