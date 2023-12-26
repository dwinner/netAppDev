using System.Collections.Generic;

namespace Contract
{
   public interface ICalculator
   {
      string AddInName { get; }

      IList<IOperation> GetOperations();

      double Operate(IOperation operation, double[] operands);
   }
}