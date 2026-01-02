using System;

namespace UseOf_Statics.Failures;

public class NotTypeExceptionFailure(Type type) : IAmFailure
{
   public string Reason { get; set; } = $"Function did not return expected type of '{type}'";

   public static IAmFailure Create(Type type) => new NotTypeExceptionFailure(type);
}