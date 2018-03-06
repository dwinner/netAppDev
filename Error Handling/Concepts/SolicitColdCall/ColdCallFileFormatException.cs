using System;
using System.Runtime.Serialization;

namespace SolicitColdCall
{
   [Serializable]
   public class ColdCallFileFormatException : Exception
   {
      public ColdCallFileFormatException()
      {
      }

      public ColdCallFileFormatException(string message)
         : base(message)
      {
      }

      public ColdCallFileFormatException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected ColdCallFileFormatException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}