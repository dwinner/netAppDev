using System;
using System.Runtime.Serialization;

namespace CultureDemo
{
   [Serializable]
   public class ParentCultureException : Exception
   {
      public ParentCultureException()
      {
      }

      public ParentCultureException(string message)
         : base(message)
      {
      }

      public ParentCultureException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected ParentCultureException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}