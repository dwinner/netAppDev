using Customers.Extensions;

namespace Customers
{
	public sealed class CustomerReflectionEmit : Customer
	{
		public override string ToString()
		{
			return this.ToStringReflectionEmit();
		}
	}
}
