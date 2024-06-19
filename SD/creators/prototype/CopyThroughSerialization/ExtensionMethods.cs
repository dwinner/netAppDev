using System.Xml.Serialization;

namespace CopyThroughSerialization;

public static class ExtensionMethods
{
/*
   public static T DeepCopy<T>(this T self)
   {
      using (var stream = new MemoryStream())
      {
         BinaryFormatter formatter = new BinaryFormatter();
         formatter.Serialize(stream, self);
         stream.Seek(0, SeekOrigin.Begin);
         object copy = formatter.Deserialize(stream);
         return (T) copy;
      }
   }
*/

   public static T? DeepCopyXml<T>(this T self)
   {
      using var memoryStream = new MemoryStream();
      var serializer = new XmlSerializer(typeof(T));
      serializer.Serialize(memoryStream, self);
      memoryStream.Position = 0;
      return (T?)serializer.Deserialize(memoryStream);
   }
}