using System;

namespace IpcRemoting.Lib;

public class EventSink : MarshalByRefObject
{
   public void StatusHandler(object sender, StatusEventArgs e)
   {
      Console.WriteLine("EventSink: Event occured: {0}", e.Message);
   }
}