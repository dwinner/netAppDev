using System;

namespace UseOf_Statics.Failures;

public class ExceptionFailure(Exception e) : Exception, IAmFailure
{
   public string Reason { get; set; } = e.Message;
}