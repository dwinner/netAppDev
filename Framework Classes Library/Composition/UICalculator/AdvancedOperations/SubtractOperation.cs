using System.Composition;
using CalculatorContract;

namespace AdvancedOperations
{
	[Export("Subtract", typeof(IBinaryOperation))]
	public class SubtractOperation : IBinaryOperation
	{
		public double Operation(double x, double y) => x - y;
	}
}