using System.Diagnostics;

namespace InjectedLoggerApp
{
	public sealed class EventViewerWriter : IWriter
	{
		public void Write(string aMessage)
			=> EventLog.WriteEntry("TestUnity", aMessage, EventLogEntryType.Information);
	}
}