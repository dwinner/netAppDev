using System;
using System.Windows.Input;

namespace _03_ImageProcessorPipeline.ViewModels
{
   public class DelegateCommand : ICommand
   {
      private readonly Action<object> _command;
      private bool _canExecute;

      public DelegateCommand(Action<object> command, bool canExecute = true)
      {
         _command = command;
         _canExecute = canExecute;
      }

      public bool IsCanExecute
      {
         set
         {
            _canExecute = value;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
         }
      }

      public bool CanExecute(object parameter) => _canExecute;

      public void Execute(object parameter) => _command?.Invoke(parameter);

      public event EventHandler CanExecuteChanged;
   }
}