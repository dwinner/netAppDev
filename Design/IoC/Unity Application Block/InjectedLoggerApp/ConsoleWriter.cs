using System;

namespace InjectedLoggerApp
{
	public sealed class ConsoleWriter : IWriter
	{
		public void Write(string aMessage) => Console.WriteLine(aMessage);
	}
}