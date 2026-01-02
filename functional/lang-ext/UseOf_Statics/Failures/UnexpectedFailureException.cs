using System;

namespace UseOf_Statics.Failures;

public class UnexpectedFailureException(IAmFailure failure) : Exception(failure.Reason), IAmFailure
{
   public string Reason { get; set; } = failure.Reason;
   public static IAmFailure Create(IAmFailure failure) => new UnexpectedFailureException(failure);
}