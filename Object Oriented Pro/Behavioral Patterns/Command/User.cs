using System;
using System.Collections.Generic;

namespace Command
{
   /// <summary>
   ///    "Вызывающий" команды
   /// </summary>
   public class User : ISaver
   {
      private readonly Calculator _calculator;

      private readonly List<IExecuteCommand> _commandHistory;
      private int _current;

      private User(Calculator calculator, List<IExecuteCommand> commandHistory)
      {
         _calculator = calculator;
         _commandHistory = commandHistory;
      }

      public User()
         : this(new Calculator(), new List<IExecuteCommand>())
      {
      }

      public void Redo(int levels)
      {
         Console.WriteLine("Redo {0} levels", levels);

         // execute redo operation
         for (var i = 0; i < levels; i++)
            if (_current < _commandHistory.Count - 1)
               _commandHistory[_current++].Execute();
      }

      public void Undo(int levels)
      {
         Console.WriteLine("Redo {0} levels", levels);

         // execute undo operation
         for (var i = 0; i < levels; i++)
            if (_current > 0)
               _commandHistory[--_current].UnExecute();
      }

      public void Compute(char @operator, int operand)
      {
         // create the command and execute it
         var command = new CalculatorCommand(@operator, operand, _calculator);
         command.Execute();

         // Add operation to the cancel list
         _commandHistory.Add(command);
         _current++;
      }
   }
}