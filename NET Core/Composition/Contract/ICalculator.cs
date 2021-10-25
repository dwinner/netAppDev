using System.Collections.Generic;

namespace Contract
{
   public interface ICalculator
   {
      IList<IOperation> GetOperations();

      double Operate(IOperation operation, double[] operands);
   }
}