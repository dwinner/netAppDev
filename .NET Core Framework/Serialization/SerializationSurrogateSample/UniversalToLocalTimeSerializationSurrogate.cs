using System;
using System.Runtime.Serialization;

namespace SerializationSurrogateSample
{
   internal sealed class UniversalToLocalTimeSerializationSurrogate : ISerializationSurrogate
   {
      private const string DateId = "Date";

      public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
      {
         // Переход от локального к мировому времени
         info.AddValue(DateId, ((DateTime)obj).ToUniversalTime().ToString("u"));
      }

      public object SetObjectData(object obj, SerializationInfo info, StreamingContext context,
         ISurrogateSelector selector)
      {
         // Переход от мирового времени к локальному
         return DateTime.ParseExact(info.GetString(DateId), "u", null).ToLocalTime();
      }
   }
}