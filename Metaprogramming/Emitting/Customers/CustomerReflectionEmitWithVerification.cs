using Customers.Extensions;

namespace Customers
{
	public sealed class CustomerReflectionEmitWithVerification 
		: Customer
	{
		public override string ToString()
		{
			return this.ToStringReflectionEmitWithVerification();
		}
	}
}
