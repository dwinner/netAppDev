using System;
using System.AddIn.Pipeline;
using CalcContract;
using CalcView;

namespace CalcAddInAdapter
{
   internal class OperationViewToContractAddInAdapter : ContractBase, IOperationContract
   {
      private readonly Operation _view;

      private OperationViewToContractAddInAdapter(Operation view)
      {
         _view = view;
      }

      public string Name { get { return _view.Name; } }

      public int NumberOperands { get { return _view.NumberOperands; } }

      public static IOperationContract ViewToContractAdapter(Operation view)
      {
         return new OperationViewToContractAddInAdapter(view);
      }

      public static Operation ContractToViewAdapter(IOperationContract contract)
      {
         var addInAdapter = contract as OperationViewToContractAddInAdapter;
         if (addInAdapter != null)
            return addInAdapter._view;

         throw new InvalidOperationException("contract is null");
      }
   }
}