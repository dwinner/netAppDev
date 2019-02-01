using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace GenExceptionSample
{
   [Serializable]
   public sealed class GenException<TExceptionArgs> : Exception, IEquatable<GenException<TExceptionArgs>>
      where TExceptionArgs : GenExceptionArgs
   {
      private const string ArgsLabel = "Args"; // Для (де)сериализации
      private readonly TExceptionArgs _exceptionArgs;

      public GenException(string message = null, Exception innerException = null)
         : this(null, message, innerException)
      {
      }

      public GenException(TExceptionArgs args, string message = null, Exception innerException = null)
         : base(message, innerException)
      {
         _exceptionArgs = args;
      }

      public GenException()
      {
      }

      public GenException(string message)
         : base(message)
      {
      }

      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
      private GenException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
         _exceptionArgs = (TExceptionArgs)info.GetValue(ArgsLabel, typeof(TExceptionArgs));
      }

      public TExceptionArgs ExceptionArgs
      {
         get { return _exceptionArgs; }
      }

      public override string Message
      {
         get
         {
            var baseMessage = base.Message;
            return _exceptionArgs == null
               ? baseMessage
               : string.Format("{0} ({1})", baseMessage, _exceptionArgs.Message);
         }
      }

      public bool Equals(GenException<TExceptionArgs> other)
      {
         return !ReferenceEquals(null, other) &&
                (ReferenceEquals(this, other) ||
                 EqualityComparer<TExceptionArgs>.Default.Equals(_exceptionArgs, other._exceptionArgs));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         info.AddValue(ArgsLabel, _exceptionArgs);
         base.GetObjectData(info, context);
      }

      public override bool Equals(object obj)
      {
         return !ReferenceEquals(null, obj) &&
                (ReferenceEquals(this, obj) ||
                 obj is GenException<TExceptionArgs> && Equals((GenException<TExceptionArgs>)obj));
      }

      public override int GetHashCode()
      {
         return EqualityComparer<TExceptionArgs>.Default.GetHashCode(_exceptionArgs);
      }

      public static bool operator ==(GenException<TExceptionArgs> left, GenException<TExceptionArgs> right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(GenException<TExceptionArgs> left, GenException<TExceptionArgs> right)
      {
         return !Equals(left, right);
      }
   }
}