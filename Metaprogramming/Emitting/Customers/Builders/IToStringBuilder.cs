namespace Customers.Builders
{
	public interface IToStringBuilder
	{
		string ToString<T>(T target);
	}
}
