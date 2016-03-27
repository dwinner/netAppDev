/**
 * Сериализация объектов
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

namespace SerializeVertex
{
   class Program
   {
      static void Main()
      {
         Vertex3D vertex3D = new Vertex3D(1.0, 2.0, 3.0) { Id = null };
         Vertex3D vertex3D2;

         byte[] bytes = new byte[1024];
         string result;
         using (MemoryStream stream = new MemoryStream(bytes))
         {
            SoapFormatter formatter = new SoapFormatter();
            formatter.Serialize(stream, vertex3D);
            result = Encoding.UTF8.GetString(bytes, 0, (int)stream.Position);

            stream.Position = 0;
            vertex3D2 = (Vertex3D)formatter.Deserialize(stream);
         }

         Console.WriteLine("Vertex: {0}", vertex3D);
         Console.WriteLine("Serialized to SOAP: " + Environment.NewLine + result);
         Console.WriteLine("Deserialized: {0}", vertex3D2);

         Console.ReadKey();
      }
   }
}
