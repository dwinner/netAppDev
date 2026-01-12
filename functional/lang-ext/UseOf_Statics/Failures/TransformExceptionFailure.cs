using System;

namespace UseOf_Statics.Failures;

public class TransformExceptionFailure(string message) : IAmFailure
{
   public Exception Exception { get; set; }

   public string Reason { get; set; } = message;

   public override string ToString() => Reason;

   public static IAmFailure Create(string msg) => new TransformExceptionFailure(msg);
}