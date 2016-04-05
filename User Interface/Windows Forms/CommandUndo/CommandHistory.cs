using System.Collections.Generic;

namespace CommandUndo
{
   /// <summary>
   /// История выполнения команд
   /// </summary>
   public class CommandHistory
   {
      private readonly Stack<ICommand> _stack = new Stack<ICommand>();

      public bool CanUndo
      {
         get
         {
            return _stack.Count > 0;
         }
      }

      public string MostRecentCommandName
      {
         get
         {
            if (!CanUndo)
            {
               return string.Empty;
            }

            ICommand command = _stack.Peek();
            return command.Name;
         }
      }

      public void PushCommand(ICommand command)
      {
         _stack.Push(command);
      }

      public ICommand PopCommand()
      {
         return _stack.Pop();
      }
   }
}
