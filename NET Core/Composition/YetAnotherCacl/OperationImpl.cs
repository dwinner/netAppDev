using Contract;

namespace YetAnotherCacl
{
   public class OperationImpl : IOperation
   {
      public string Name { get; internal init; }

      public int NumberOperands { get; internal init; }
   }
}