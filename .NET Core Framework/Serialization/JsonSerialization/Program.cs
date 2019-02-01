// Сериализация в формате Json

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace JsonSerialization
{
   internal static class Program
   {
      private static void Main()
      {
         var person = new Person {Name = "Denny", Age = 30};
         Person o;
         using (var stream = new MemoryStream())
         {
            var serializer = new DataContractJsonSerializer(typeof(Person));
            serializer.WriteObject(stream, person);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            Console.WriteLine(reader.ReadToEnd());

            stream.Position = 0;
            o = (Person) serializer.ReadObject(stream);
         }

         Console.WriteLine(o);
      }
   }

   [DataContract]
   public sealed class Person
   {
      [DataMember]
      public string Name { get; set; }

      [DataMember]
      public int Age { get; set; }

      public override string ToString() => $"Name: {Name}, Age: {Age}";
   }
}