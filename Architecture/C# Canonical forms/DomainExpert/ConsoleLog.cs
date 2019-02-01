using System;

namespace DomainExpert
{
   public class ConsoleLog : ILog
   {
      public void Report(string aMessage)
      {
         Console.WriteLine(aMessage);
      }
   }
}