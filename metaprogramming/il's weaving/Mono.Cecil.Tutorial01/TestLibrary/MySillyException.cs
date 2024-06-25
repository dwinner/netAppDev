using System;
using System.Runtime.Serialization;
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace TestLibrary
{
   [Serializable]
   public abstract class MySillyException : Exception
   {
      protected MySillyException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }

      public override string Message { get; }

      public int Number { get; }
   }
}