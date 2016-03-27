using System;
using System.Collections.Generic;

namespace Command
{
   /// <summary>
   /// "Вызывающий" команды
   /// </summary>
   public class User : ISaver
   {
      private int _current;

      public Calculator Calculator
      {
         get { return _calculator; }
      }
      private readonly Calculator _calculator;

      public List<IExecuteCommand> CommandHistory
      {
         get { return _commandHistory; }
      }
      private readonly List<IExecuteCommand> _commandHistory;

      public User(Calculator calculator, List<IExecuteCommand> commandHistory)
      {
         _calculator = calculator;
         _commandHistory = commandHistory;
      }

      public User(User otherUser)
      {
         _calculator = new Calculator(otherUser._current);
         _commandHistory = new List<IExecuteCommand>(otherUser.CommandHistory);
      }

      public User()
         : this(new Calculator(), new List<IExecuteCommand>())
      { }

      public void Compute(char @operator, int operand)
      {
         // Создаем команду операции и выполняем ее
         var command = new CalculatorCommand(@operator, operand, _calculator);
         command.Execute();

         // Добавляем операцию к списку отмены
         _commandHistory.Add(command);
         _current++;
      }

      public void Redo(int levels)
      {
         Console.WriteLine("Redo {0} levels", levels);

         // Делаем возврат операций
         for (int i = 0; i < levels; i++)
         {
            if (_current < _commandHistory.Count - 1)
            {
               _commandHistory[_current++].Execute();
            }
         }
      }

      public void Undo(int levels)
      {
         Console.WriteLine("Redo {0} levels", levels);

         // Делаем отмену операций
         for (int i = 0; i < levels; i++)
         {
            if (_current > 0)
            {
               _commandHistory[--_current].UnExecute();
            }
         }
      }
   }
}