using System.AddIn.Pipeline;
using AddInView;
using Contract;
using JetBrains.Annotations;

namespace AddInSideAdapter
{
   public class HostObjectContractToViewAddInAdapter : HostObject
   {
      private readonly IHostObjectContract _contract;
      [UsedImplicitly] private ContractHandle _handle;

      public HostObjectContractToViewAddInAdapter(IHostObjectContract contract)
      {
         _contract = contract;
         _handle = new ContractHandle(contract);
      }

      public override void ReportProgress(int progressPercent)
      {
         _contract.ReportProgress(progressPercent);
      }
   }
}