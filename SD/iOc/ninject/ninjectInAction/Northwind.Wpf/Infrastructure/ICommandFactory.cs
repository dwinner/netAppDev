using System;
using System.Windows.Input;

namespace Northwind.Wpf.Infrastructure
{
   public interface ICommandFactory
   {
      ICommand CreateCommand(Action<object> action, Func<object, bool> canExecute = null);
   }
}