using System.AddIn.Contract;

namespace CalcContract
{
   public interface IOperationContract : IContract
   {
      string Name { get; }
      int NumberOperands { get; }
   }
}