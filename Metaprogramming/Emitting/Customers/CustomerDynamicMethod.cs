using Customers.Extensions;
using Customers.Lib;

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
