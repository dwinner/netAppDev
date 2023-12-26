using System;

public class AnotherCustomException : Exception
{
   public AnotherCustomException(string message, Exception innerException)
      : base(message, innerException)
   {
   }
}