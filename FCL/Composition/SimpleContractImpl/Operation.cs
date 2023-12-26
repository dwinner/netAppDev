using Contract;

namespace SimpleContractImpl
{
   public class Operation : IOperation
   {
      public string Name { get; internal init; }

      public int NumberOperands { get; internal init; }
   }
}