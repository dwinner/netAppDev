using System;
using System.Runtime.Serialization;

namespace SolicitColdCall
{
   [Serializable]
   public class UnexpectedException : Exception
   {
      public UnexpectedException()
      {
      }

      public UnexpectedException(string message)
         : base(message)
      {
      }

      public UnexpectedException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected UnexpectedException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}