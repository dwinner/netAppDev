using System;
using System.AddIn;
using System.Collections.Generic;
using AdvancedCalcAddIn.Annotations;
using CalcView;

namespace AdvancedCalcAddIn
{
   [AddIn("Advanced Calc", Publisher = "Wrox Press", Version = "1.1.0.0", Description = "Sample AddIn"), UsedImplicitly]
   public class AdvancedCalculatorV1 : Calculator
   {
      private readonly List<Operation> _operations;

      public AdvancedCalculatorV1()
      {
         _operations = new List<Operation>
         {
            new Operation {Name = "+", NumberOperands = 2},
            new Operation {Name = "-", NumberOperands = 2},
            new Operation {Name = "/", NumberOperands = 2},
            new Operation {Name = "*", NumberOperands = 2},
            new Operation {Name = "++", NumberOperands = 1},
            new Operation {Name = "--", NumberOperands = 1},
            new Operation {Name = "%", NumberOperands = 2}
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
            case "++":
               return ++operand[0];
            case "--":
               return --operand[0];
            case "%":
               return operand[0] % operand[1];
            default:
               throw new InvalidOperationException(string.Format("invalid operation {0}", operation.Name));
         }
      }
   }
}