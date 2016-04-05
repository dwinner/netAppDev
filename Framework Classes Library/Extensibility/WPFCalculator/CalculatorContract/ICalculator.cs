using System.Collections.Generic;

namespace CalculatorContract
{
   /// <summary>
   /// Интерфейс для экспорта
   /// </summary>
   public interface ICalculator
   {
      IList<IOperation> GetOperations();

      double Operate(IOperation operation, double[] operands);
   }
}