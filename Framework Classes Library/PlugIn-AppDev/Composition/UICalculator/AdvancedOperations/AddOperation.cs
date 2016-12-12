using System.Composition;
using CalculatorContract;
using CalculatorUtils;

namespace AdvancedOperations
{
	[Export("Add", typeof(IBinaryOperation))]
	[SpeedMetadata(Speed = Speed.Fast)]
	public class AddOperation : IBinaryOperation
	{
		public double Operation(double x, double y) => x + y;
	}
}