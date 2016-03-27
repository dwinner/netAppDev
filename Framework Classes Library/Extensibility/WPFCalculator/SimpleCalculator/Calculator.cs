using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CalculatorContract;
using CalculatorUtils;

namespace SimpleCalculator
{
   [Export(typeof (ICalculator))]
   public class Calculator : ICalculator
   {
      [ImportMany("Add", typeof (Func<double, double, double>))]
      public Lazy<Func<double, double, double>, ISpeedCapabilities>[] AddMethods { get; set; }

      [Import("Subtract", typeof (Func<double, double, double>))]
      public Func<double, double, double> Subtract { get; set; }

      public IList<IOperation> GetOperations()
      {
         return new List<IOperation>
         {
            new Operation {Name = "+", NumberOperands = 2},
            new Operation {Name = "-", NumberOperands = 2},
            new Operation {Name = "/", NumberOperands = 2},
            new Operation {Name = "*", NumberOperands = 2}
         };
      }

      public double Operate(IOperation operation, double[] operands)
      {
         double result = 0;
         switch (operation.Name)
         {
            case "+":
               foreach (var addMethod in AddMethods.Where(addMethod => addMethod.Metadata.Speed == Speed.Fast))
                  result = addMethod.Value(operands[0], operands[1]);
               break;
            case "-":
               result = Subtract(operands[0], operands[1]);
               break;
            case "/":
               result = operands[0]/operands[1];
               break;
            case "*":
               result = operands[0]*operands[1];
               break;
            default:
               throw new InvalidOperationException(
                  String.Format("invalid operation {0}", operation.Name));
         }

         return result;
      }
   }
}