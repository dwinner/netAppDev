using System.Collections.Generic;

namespace CalculatorContract
{
	public interface ICalculator
	{
		IEnumerable<IOperation> GetOperations();
		double Operate(IOperation operation, double[] operands);
	}
}