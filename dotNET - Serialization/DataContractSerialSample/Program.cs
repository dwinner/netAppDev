/**
 * Simple data contract serialization
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace DataContract.SerialSample
{
   internal static class Program
   {
      private static void Main()
      {
         SimpleSerialize();
         Formatting();
         BinaryXmlFormatting();
      }

      #region Binary Xml format

      private static void BinaryXmlFormatting()
      {
         var person = new Person {Name = "Stacey", Age = 30};
         var serializer = new DataContractSerializer(typeof(Person));

         // Serialization
         MemoryStream toMemStream;
         using (toMemStream = new MemoryStream())
         using (var writer = XmlDictionaryWriter.CreateBinaryWriter(toMemStream))
         {
            serializer.WriteObject(writer, person);
         }

         // Deserialization
         using (var fromMemStream = new MemoryStream(toMemStream.ToArray()))
         using (var reader = XmlDictionaryReader.CreateBinaryReader(fromMemStream, XmlDictionaryReaderQuotas.Max))
         {
            person = (Person) serializer.ReadObject(reader);
         }

         Console.WriteLine(person);
      }

      #endregion

      #region Formatting

      private static void Formatting()
      {
         const string formattedXml = "formattedPerson.xml";

         var person = new Person {Name = "Stacey", Age = 30};
         var serializer = new DataContractSerializer(typeof(Person));

         // Formatting strategy
         var settings = new XmlWriterSettings {Indent = true};

         using (var writer = XmlWriter.Create(formattedXml, settings))
         {
            serializer.WriteObject(writer, person);
            Process.Start(formattedXml);
         }
      }

      #endregion

      #region Simple serialization via data contract

      private static void SimpleSerialize()
      {
         const string personXml = "person.xml";

         var person = new Person {Name = "Stacey", Age = 30};
         var serializer = new DataContractSerializer(typeof(Person));

         // Serialize
         using (var stream = File.Create(personXml))
         {
            serializer.WriteObject(stream, person);
         }

         // Deserialize
         using (var stream = File.OpenRead(personXml))
         {
            person = (Person) serializer.ReadObject(stream);
         }

         Console.WriteLine(person);
      }

      #endregion
   }
}