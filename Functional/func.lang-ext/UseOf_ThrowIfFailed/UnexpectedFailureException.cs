using System;

namespace UseOf_ThrowIfFailed;

public class UnexpectedFailureException(IAmFailure failure) : Exception
{
   private readonly IAmFailure _failure = failure;
}