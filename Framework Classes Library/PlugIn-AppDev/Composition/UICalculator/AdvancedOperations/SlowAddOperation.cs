using System.Composition;
using System.Threading.Tasks;
using CalculatorContract;
using CalculatorUtils;

namespace AdvancedOperations
{
	[Export("Add", typeof(IBinaryOperation))]
	[SpeedMetadata(Speed = Speed.Slow)]
	public class SlowAddOperation : IBinaryOperation
	{
		public double Operation(double x, double y)
		{
			Task.Delay(3000).Wait();
			return x + y;
		}
	}
}