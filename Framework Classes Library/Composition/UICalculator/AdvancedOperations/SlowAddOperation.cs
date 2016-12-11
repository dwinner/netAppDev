using System.Composition;
using System.Threading.Tasks;
using CalculatorContract;

namespace AdvancedOperations
{
	[Export("Add", typeof(IBinaryOperation))]
	public class SlowAddOperation : IBinaryOperation
	{
		public double Operation(double x, double y)
		{
			Task.Delay(3000).Wait();
			return x + y;
		}
	}
}