using Customers.Extensions;

namespace Customers.Builders
{
	public sealed class ToStringReflectionEmitBuilder
		: IToStringBuilder
	{
		public string ToString<T>(T target)
		{
			return target.ToStringReflectionEmit();
		}
	}
}
