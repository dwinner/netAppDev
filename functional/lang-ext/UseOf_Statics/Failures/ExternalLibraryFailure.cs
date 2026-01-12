using System;

namespace UseOf_Statics.Failures;

public class ExternalLibraryFailure : IAmFailure
{
   public ExternalLibraryFailure(Exception exception)
   {
      Reason = exception.Message;
      Exception = exception;
   }

   public ExternalLibraryFailure(string message) => Reason = message;

   public Exception Exception { get; }

   public string Reason { get; set; }

   public static IAmFailure Create(string message) => new ExternalLibraryFailure(message);
   public static IAmFailure Create(Exception e) => new ExternalLibraryFailure(e);
}