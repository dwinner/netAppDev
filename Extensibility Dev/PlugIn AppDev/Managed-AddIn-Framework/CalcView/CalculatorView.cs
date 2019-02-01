using System.AddIn.Pipeline;
using System.Collections.Generic;

namespace CalcView
{
   [AddInBase]
   public abstract class Calculator
   {
      public abstract IList<Operation> GetOperations();
      public abstract double Operate(Operation operation, double[] operand);
   }
}