using System;

namespace Entry
{
   [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
   public sealed class LogAttribute : Attribute
   {
      public string Message { get; set; }

      public LogAttribute(string message)
      {
         Message = message;
      }

      public LogAttribute()
      {         
      }
   }
}