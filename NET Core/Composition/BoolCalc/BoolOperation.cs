using Contract;

namespace BoolCalc
{
   public class BoolOperation : IOperation
   {
      public string Name { get; internal init; }

      public int NumberOperands { get; internal init; }
   }
}