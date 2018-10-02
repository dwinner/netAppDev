// Сериализация больших объектов

using ProtoBuf;
using System;
using System.IO;
using ProtocolBufferSerializer = ProtoBuf.Serializer;

namespace BigObjectsSerialization
{
   internal static class Program
   {
      private static void Main()
      {
         // Сериализация
         var person = new Person
         {
            Id = 12345,
            Name = "Fred",
            Address = new Address
            {
               Line1 = "Flat 1",
               Line2 = "The Meadows"
            }
         };
         using (Stream file = File.Create("person.bin"))
         {
            ProtocolBufferSerializer.Serialize(file, person);
         }

         // Десериализация
         using (Stream file = File.OpenRead("person.bin"))
         {
            var newPerson = ProtocolBufferSerializer.Deserialize<Person>(file);
            Console.WriteLine(newPerson);
         }
      }
   }

   [ProtoContract]
   internal class Person
   {
      [ProtoMember(1)]
      public int Id { private get; set; }

      [ProtoMember(2)]
      public string Name { private get; set; }

      [ProtoMember(3)]
      public Address Address { private get; set; }

      public override string ToString()
      {
         return $"Id: {Id}, Name: {Name}, Address: {Address}";
      }
   }

   [ProtoContract]
   internal class Address
   {
      [ProtoMember(1)]
      public string Line1 { private get; set; }

      [ProtoMember(2)]
      public string Line2 { private get; set; }

      public override string ToString()
      {
         return $"Line1: {Line1}, Line2: {Line2}";
      }
   }
}