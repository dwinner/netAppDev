using CalcAddInAdapter.Properties;
using CalcContract;
using CalcView;
using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace CalcAddInAdapter
{
   [AddInAdapter, UsedImplicitly]
   internal class CalculatorViewToContractAddInAdapter : ContractBase, ICalculatorContract
   {
      private readonly Calculator _view;

      public CalculatorViewToContractAddInAdapter(Calculator view)
      {
         _view = view;
      }

      public IListContract<IOperationContract> GetOperations()
      {
         return CollectionAdapters.ToIListContract(_view.GetOperations(),
            OperationViewToContractAddInAdapter.ViewToContractAdapter,
            OperationViewToContractAddInAdapter.ContractToViewAdapter);
      }

      public double Operate(IOperationContract operation, double[] operands)
      {
         return _view.Operate(OperationViewToContractAddInAdapter.ContractToViewAdapter(operation), operands);
      }
   }
}