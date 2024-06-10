using System;

namespace Infrastructure
{
    public class NorthwindExceptionHandlerService : ExceptionHandlerService
    {
        public override void HandleException(Exception exception)
        {
            throw new NorthwindException("Exception occurred!", exception);
        }
    }
}