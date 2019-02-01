using System;

namespace Entry
{
   public class LoggingImpl : ILogging
   {
      public void Log(string aMessage)
      {
         Console.WriteLine(aMessage);
      }
   }
}