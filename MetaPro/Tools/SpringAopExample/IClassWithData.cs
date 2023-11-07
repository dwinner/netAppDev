using System;

namespace SpringAopExample
{
	public interface IClassWithData
	{
		Guid GetData();
		Guid Data { get; }
	}
}
