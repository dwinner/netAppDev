using System.AddIn.Pipeline;
using CalcContract;
using HostAdapter.Annotations;
using HostView;

namespace HostAdapter
{
   internal class OperationContractToViewHostAdapter : Operation
   {
      [UsedImplicitly]
      private ContractHandle _handle;

      public OperationContractToViewHostAdapter(IOperationContract contract)
      {
         Contract = contract;
         _handle = new ContractHandle(contract);
      }

      public IOperationContract Contract { get; private set; }

      public override string Name
      {
         get { return Contract.Name; }
      }

      public override int NumberOperands
      {
         get { return Contract.NumberOperands; }
      }
   }
}