using Customers.Builders;

namespace Customers
{
	public sealed class CustomerDependencyInjected
		: Customer
	{
		public CustomerDependencyInjected(IToStringBuilder builder)
			: base()
		{
			this.Builder = builder;
		}

		public override string ToString()
		{
			return this.Builder.ToString(this);
		}

		private IToStringBuilder Builder { get; set; }
	}
}
