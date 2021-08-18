using System;
using System.Runtime.Serialization;

namespace Player.Core
{
   [Serializable]
   public class CodecNotFoundException : Exception
   {
      public CodecNotFoundException()
      {
      }

      public CodecNotFoundException(string message) : base(message)
      {
      }

      public CodecNotFoundException(string message, Exception inner) : base(message, inner)
      {
      }

      protected CodecNotFoundException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}