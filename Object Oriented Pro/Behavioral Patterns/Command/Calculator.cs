using System;

namespace Command
{
   /// <summary>
   /// Получатель команд
   /// </summary>
   public class Calculator
   {
      private int _current;

      public Calculator(int current)
      {
         _current = current;
      }

      public Calculator()
         : this(0)
      { }

      public void Operation(char anOperator, int operand)
      {
         switch (anOperator)
         {
            case '+':
               _current += operand;
               break;

            case '-':
               _current -= operand;
               break;

            case '*':
               _current *= operand;
               break;

            case '/':
               _current /= operand;
               break;

            default:
               throw new InvalidOperationException("Unknown command");
         }

         LogResult(anOperator, operand);
      }

      protected virtual void LogResult(char @operator, int operand)
      {
         Console.WriteLine("Current value = {0,3} (following {1} {2})", _current, @operator, operand);
      }
   }
}