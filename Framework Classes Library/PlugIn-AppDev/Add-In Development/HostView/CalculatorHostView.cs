using System.Collections.Generic;

namespace HostView
{
   public abstract class Calculator
   {
      public abstract IList<Operation> GetOperations();
      public abstract double Operate(Operation operation, params double[] operand);
   }
}