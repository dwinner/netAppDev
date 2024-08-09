using System;

namespace RxLibrary;

public class Alert
{
   public Alert(string message, DateTimeOffset time)
   {
      Message = message;
      Time = time;
   }

   public string Message { get; set; }

   public DateTimeOffset Time { get; set; }
}