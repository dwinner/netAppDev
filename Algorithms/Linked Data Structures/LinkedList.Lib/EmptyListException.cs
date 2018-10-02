using System;
using System.Runtime.Serialization;

namespace LinkedList.Lib
{
   [Serializable]
   public class EmptyListException : Exception
   {
      public EmptyListException()
         : base("The list is empty")
      {
      }

      public EmptyListException(string message)
         : base($"The {message} is empty")
      {
      }

      public EmptyListException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected EmptyListException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}