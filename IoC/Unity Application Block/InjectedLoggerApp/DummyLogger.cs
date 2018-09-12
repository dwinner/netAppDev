using Microsoft.Practices.Unity;

namespace InjectedLoggerApp
{
	public sealed class DummyLogger
	{
		[Dependency]
		public IWriter SelectedWriter { get; set; }

		public void WriteOutput(string aMessage) => SelectedWriter.Write(aMessage);
	}
}