using System;

namespace GzipXmlSerializationSample
{
   public interface ICapableSerialize<T> : IDisposable
   {
      void Serialize(T obj);
      T Deserialize();
   }
}