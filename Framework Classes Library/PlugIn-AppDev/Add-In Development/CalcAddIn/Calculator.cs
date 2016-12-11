using System;
using System.AddIn;
using System.Collections.Generic;
using CalcAddIn.Annotations;
using CalcView;

namespace CalcAddIn
{
   [AddIn("Simple Calc", Publisher = "Wrox Press", Version = "1.0.0.0", Description = "Sample AddIn"), UsedImplicitly]
   public class CalculatorV1 : Calculator
   {
      private readonly List<Operation> _operations;

      public CalculatorV1()
      {
         _operations = new List<Operation>
         {
            new Operation {Name = "+", NumberOperands = 2},
            new Operation {Name = "-", NumberOperands = 2},
            new Operation {Name = "/", NumberOperands = 2},
            new Operation {Name = "*", NumberOperands = 2}
         };
      }

      public override IList<Operation> GetOperations()
      {
         return _operations;
      }

      public override double Operate(Operation operation, double[] operand)
      {
         switch (operation.Name)
         {
            case "+":
               return operand[0] + operand[1];
            case "-":
               return operand[0] - operand[1];
            case "/":
               return operand[0] / operand[1];
            case "*":
               return operand[0] * operand[1];
            default:
               throw new InvalidOperationException(String.Format("invalid operation {0}", operation.Name));
         }
      }
   }
}