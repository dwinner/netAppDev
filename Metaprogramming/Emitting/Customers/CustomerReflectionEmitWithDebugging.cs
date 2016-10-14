using Customers.Extensions;

namespace Customers
{
	public sealed class CustomerReflectionEmitWithDebugging : Customer
	{
		public override string ToString()
		{
			return this.ToStringReflectionEmitWithDebugging();
		}
	}
}
