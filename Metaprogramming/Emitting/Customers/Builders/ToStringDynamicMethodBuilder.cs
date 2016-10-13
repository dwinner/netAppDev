using Customers.Extensions;

namespace Customers.Builders
{
	public sealed class ToStringDynamicMethodBuilder
		: IToStringBuilder
	{
		public string ToString<T>(T target)
		{
			return target.ToStringDynamicMethod();
		}
	}
}
