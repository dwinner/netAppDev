using Customers.Lib;

namespace ExpressionSamples
{
	public sealed class CustomerExpressions : Customer
	{
		public override string ToString()
		{
			return this.ToStringExpression();
		}
	}
}
