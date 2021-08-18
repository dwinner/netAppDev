using System;

namespace Infrastructure
{
    public abstract class ExceptionHandlerService
    {
        public abstract void HandleException(Exception exception);
    }
}