using System;
using System.Windows.Input;

namespace MVVMDemo
{
   // Это распространенный вид класса, известный также под именем RelayCommand
   public class DelegateCommand : ICommand
   {
      // Делегаты, управляющие командой
      private readonly Action<object> _execute;
      private readonly Predicate<object> _canExecute;

      public DelegateCommand(Action<object> executeDelegate)
         : this(executeDelegate, null)
      {
      }

      public DelegateCommand(Action<object> executeDelegate, Predicate<object> canExecuteDelegate)
      {
         _execute = executeDelegate;
         _canExecute = canExecuteDelegate;
      }

      public bool CanExecute(object parameter)
      {
         return _canExecute == null || _canExecute(parameter);
      }

      public event EventHandler CanExecuteChanged;

      public void Execute(object parameter)
      {
         _execute(parameter);
      }
   }
}
