using System;
using System.Runtime.Serialization;

namespace SolicitColdCall
{
   [Serializable]
   public class SalesSpyFoundException : Exception
   {
      public SalesSpyFoundException()
      {
      }

      public SalesSpyFoundException(string spyName)
         : base(string.Format("Sales spy found, with name {0}", spyName))
      {
      }

      public SalesSpyFoundException(string spyName, Exception innerEx)
         : base(string.Format("Sales spy found, with name {0}", spyName), innerEx)
      {
      }

      protected SalesSpyFoundException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}