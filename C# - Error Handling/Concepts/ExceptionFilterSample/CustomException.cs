using System;

namespace ExceptionFilterSample
{
   public sealed class CustomException : Exception
   {
      public CustomException(string message)
         : base(message)
      {
      }

      public CustomException(string message, Exception inner)
         : base(message, inner)
      {
      }

      public int ErrorCode { get; set; }
   }
}