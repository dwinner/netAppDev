
namespace Diagram
{
   public interface ICalcContract : System.AddIn.Contract.IContract
   {
      int GetOperations(int x, int y);

      void Operate();
   }
}
