using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Prototype
{
   // Note! Если нужна безопасность типов, то where T : ISerializable 
   public class DeepCopyManager<T>
   {
      public DeepCopyManager(T type)
      {
         Type = type;
         // Note! Если безопасность типов под вопросом, то добавить с условие: || typeof(T) as ISerializable == null
         if (!typeof(T).IsSerializable)
         {
            throw new NotSupportedException(
               string.Format("Non serializable type: {0}",
                  typeof(T).FullName));
         }
      }

      public T DeepCopy()
      {
         using (var memoryStream = new MemoryStream())
         {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, Type);
            memoryStream.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);
            return (T) binaryFormatter.Deserialize(memoryStream);
         }         
      }

      public T Type { get; set; }
   }
}
