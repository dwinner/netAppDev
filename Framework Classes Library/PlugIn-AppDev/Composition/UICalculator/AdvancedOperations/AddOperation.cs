using System.Composition;
using CalculatorContract;

namespace AdvancedOperations
{
	[Export("Add", typeof(IBinaryOperation))]
	public class AddOperation : IBinaryOperation
	{
		public double Operation(double x, double y) => x + y;
	}
}