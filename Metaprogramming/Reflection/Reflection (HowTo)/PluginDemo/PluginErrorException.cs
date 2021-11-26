using System;
using System.Runtime.Serialization;

namespace PluginDemo
{
   [Serializable]
   public class PluginErrorException : Exception
   {
      public PluginErrorException()
      {
      }

      public PluginErrorException(string message)
         : base(message)
      {
      }

      public PluginErrorException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected PluginErrorException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}