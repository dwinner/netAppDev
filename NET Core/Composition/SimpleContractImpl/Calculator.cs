using System;
using System.Collections.Generic;
using Contract;

namespace SimpleContractImpl
{
   public class Calculator : ICalculator
   {
      public IList<IOperation> GetOperations() =>
         new List<IOperation>
         {
            new Operation { Name = "+", NumberOperands = 2 },
            new Operation { Name = "-", NumberOperands = 2 },
            new Operation { Name = "/", NumberOperands = 2 },
            new Operation { Name = "*", NumberOperands = 2 }
         };

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
               throw new InvalidOperationException($"Invalid operation {operation.Name}");
         }

         return result;
      }
   }
}