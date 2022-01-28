using System;
using System.Collections.Generic;
using System.Composition;
using Contract;

namespace BoolCalc
{
   [Export(typeof(ICalculator))]
   public class BoolCalculator : ICalculator
   {
      public string AddInName => "Bool calculations";

      public IList<IOperation> GetOperations() =>
         new List<IOperation>
         {
            new BoolOperation { Name = "&", NumberOperands = 2 },
            new BoolOperation { Name = "^", NumberOperands = 2 },
            new BoolOperation { Name = "|", NumberOperands = 2 },
            new BoolOperation { Name = "&&", NumberOperands = 2 },
            new BoolOperation { Name = "||", NumberOperands = 2 }
         };

      public double Operate(IOperation operation, double[] operands)
      {
         bool result;
         var first = Convert.ToBoolean(operands[0]);
         var second = Convert.ToBoolean(operands[1]);

         switch (operation.Name)
         {
            case "&":
               result = first & second;
               break;
            case "^":
               result = first ^ second;
               break;
            case "|":
               result = first | second;
               break;
            case "&&":
               result = first && second;
               break;
            case "||":
               result = first || second;
               break;
            default:
               throw new InvalidOperationException($"Invalid operation {operation.Name}");
         }

         return Convert.ToDouble(result);
      }
   }
}