using System.Collections.Generic;

namespace CalculatorContract
{
	public interface ICalculator
	{
		IList<IOperation> GetOperations();
		double Operate(IOperation anOperation, double[] operands);
	}
}