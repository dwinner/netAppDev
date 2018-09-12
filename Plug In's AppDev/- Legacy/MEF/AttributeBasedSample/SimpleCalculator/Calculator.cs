using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using CalculatorContract;

namespace SimpleCalculator
{
   [Export(typeof(ICalculator))]
   public class Calculator : ICalculator
   {
      public IList<IOperation> GetOperations()
      {
         return new List<IOperation>
         {
            new Operation {Name = "+", NumberOperands = 2},
            new Operation {Name = "-", NumberOperands = 2},
            new Operation {Name = "/", NumberOperands = 2},
            new Operation {Name = "*", NumberOperands = 2},
         };
      }

      public double Operate(IOperation operation, double[] operands)
      {
         double result;
         switch (operation.Name)
         {
            case "+":
               result = operands[0] + operands[1];
               break;
            case "-":
               result = operands[0] - operands[1];
               break;
            case "/":
               result = operands[0] / operands[1];
               break;
            case "*":
               result = operands[0] * operands[1];
               break;
            default:
               throw new InvalidOperationException(string.Format("Invalid operation {0}", operation.Name));
            // Недоступная операция
         }

         return result;
      }
   }
}