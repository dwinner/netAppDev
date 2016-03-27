using System;

namespace Command
{
   /// <summary>
   /// Конкретная реализация команд
   /// </summary>
   public class CalculatorCommand : IExecuteCommand
   {
      public char TheOperator { get; set; }

      public int Operand { get; set; }

      public Calculator Calculator { get; set; }

      public CalculatorCommand(char theOperator, int operand, Calculator calculator)
      {
         TheOperator = theOperator;
         Operand = operand;
         Calculator = calculator;
      }

      public void Execute()
      {
         Calculator.Operation(TheOperator, Operand);
      }

      public void UnExecute()
      {
         Calculator.Operation(Undo(TheOperator), Operand);
      }

      private static char Undo(char theOperator)
      {
         char undoChar;

         switch (theOperator)
         {
            case '+':
               undoChar = '-';
               break;

            case '-':
               undoChar = '+';
               break;

            case '*':
               undoChar = '/';
               break;

            case '/':
               undoChar = '*';
               break;

            default:
               throw new InvalidOperationException("Unknown command");
         }

         return undoChar;
      }
   }
}