using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Prototype
{
   public static class UniversalCopyUtility<T>
   {
      public static T DeepCopy(T anyObject)
      {
         if (!typeof(T).IsSerializable)
         {            
            return default(T);
         }

         using (var memoryStream = new MemoryStream())
         {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, anyObject);
            memoryStream.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            return (T)binaryFormatter.Deserialize(memoryStream);
         }  
      }
   }
}
