using System;
using System.Runtime.Serialization;

namespace SearchAlgs.Lib
{
   [Serializable]
   public class TooBigArrayException : Exception
   {
      public TooBigArrayException()
      {
      }

      public TooBigArrayException(string message) : base(message)
      {
      }

      public TooBigArrayException(string message, Exception inner) : base(message, inner)
      {
      }

      protected TooBigArrayException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}