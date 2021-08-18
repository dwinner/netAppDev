using System;
using System.Runtime.Serialization;

namespace QuoteServer
{
   [Serializable]
   public class QuoteException : Exception
   {
      public QuoteException()
      {
      }

      public QuoteException(string message)
         : base(message)
      {
      }

      public QuoteException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected QuoteException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}