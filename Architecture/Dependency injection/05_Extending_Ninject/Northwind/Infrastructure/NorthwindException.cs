using System;

namespace Infrastructure
{
    public class NorthwindException : Exception
    {
        public NorthwindException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}