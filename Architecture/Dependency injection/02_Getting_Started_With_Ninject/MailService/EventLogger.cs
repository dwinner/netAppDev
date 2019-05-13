using System.Diagnostics;

namespace Samples.MailService
{
   internal class EventLogger : ILogger
   {
      public void Log(string message) => EventLog.WriteEntry("MailService", message);
   }
}