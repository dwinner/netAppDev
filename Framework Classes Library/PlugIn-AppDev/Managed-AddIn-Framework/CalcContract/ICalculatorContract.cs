using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace CalcContract
{
   [AddInContract]
   public interface ICalculatorContract : IContract
   {
      IListContract<IOperationContract> GetOperations();
      double Operate(IOperationContract operation, double[] operands);
   }
}