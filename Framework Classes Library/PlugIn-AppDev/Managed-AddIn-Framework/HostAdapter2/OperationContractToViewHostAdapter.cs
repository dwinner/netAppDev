using System.AddIn.Pipeline;
using CalcContract;
using HostView;

namespace HostAdapter
{
   internal class OperationContractToViewHostAdapter : Operation
   {
      // ReSharper disable once NotAccessedField.Local
      private ContractHandle _handle;

      public OperationContractToViewHostAdapter(IOperationContract contract)
      {
         Contract = contract;
         _handle = new ContractHandle(contract);
      }

      // internal IOperationContract contract;

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