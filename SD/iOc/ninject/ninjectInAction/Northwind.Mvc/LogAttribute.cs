using System;

namespace Northwind.Mvc
{
   public class LogAttribute : Attribute
   {
      public string LogLevel { get; set; }
   }
}