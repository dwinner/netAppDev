using Customers.Extensions;

namespace Customers
{
	public sealed class CustomerDynamicMethod : Customer
	{
		public override string ToString()
		{
			return this.ToStringDynamicMethod();
		}
	}
}
