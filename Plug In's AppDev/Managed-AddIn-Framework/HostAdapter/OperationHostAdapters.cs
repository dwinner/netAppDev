using CalcContract;
using HostView;

namespace HostAdapter
{
   internal static class OperationHostAdapters
   {
      internal static IOperationContract ViewToContractAdapter(Operation view)
      {
         return ((OperationContractToViewHostAdapter)view).Contract;
      }

      internal static Operation ContractToViewAdapter(IOperationContract contract)
      {
         return new OperationContractToViewHostAdapter(contract);
      }
   }
}