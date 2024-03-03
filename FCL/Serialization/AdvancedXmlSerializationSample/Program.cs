/*
 * XML serialization
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AdvancedXmlSerializationSample
{
   internal static class Program
   {
      private static void Main()
      {
         var person = new Person
         {
            Name = "John",
            Age = 30,
            HomeAddress = new Address {Street = "Revolution", PostCode = "301369"},
            Addresses = new List<Address> {new Address {Street = "street", PostCode = "postcode"}}
         };

         var serializer = new XmlSerializer(typeof(Person) /* ALT: new []{ typeof(Student),typeof(Teacher) }*/);
         const string personXml = "person.xml";

         using (var stream = File.Create(personXml))
         {
            serializer.Serialize(stream, person);
         }

         Person restoredPerson;
         using (var stream = File.OpenRead(personXml))
         {
            restoredPerson = (Person) serializer.Deserialize(stream);
         }

         Console.WriteLine(restoredPerson);
      }
   }

   [XmlInclude(typeof(Student))]
   [XmlInclude(typeof(Teacher))]
   public class Person
   {
      [XmlElement("firstName")]
      public string Name { get; set; }

      [XmlAttribute("roughAge")]
      public int Age { get; set; }

      [XmlElement(nameof(Address), typeof(Address))]
      [XmlElement(nameof(UsAddress), typeof(UsAddress))]
      [XmlElement(nameof(AuAddress), typeof(AuAddress))]
      public Address HomeAddress { get; set; }

      [XmlArray("prevAddresses")]
      [XmlArrayItem(nameof(Address), typeof(Address))]
      [XmlArrayItem(nameof(UsAddress), typeof(UsAddress))]
      [XmlArrayItem(nameof(AuAddress), typeof(AuAddress))]
      public List<Address> Addresses { get; set; } = new List<Address>();

      public override string ToString() =>
         $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}, {nameof(HomeAddress)}: {HomeAddress}";
   }

   public class Student : Person
   {
   }

   public class Teacher : Person
   {
   }

   [XmlInclude(typeof(AuAddress))]
   [XmlInclude(typeof(UsAddress))]
   public class Address : IXmlSerializable
   {
      //[XmlAttribute("street")]
      public string Street { get; set; }

      //[XmlAttribute("postalCode")]
      public string PostCode { get; set; }

      public XmlSchema GetSchema() => null;

      public void ReadXml(XmlReader reader)
      {
         reader.ReadStartElement();
         Street = reader.ReadElementContentAsString(nameof(Street), string.Empty);
         PostCode = reader.ReadElementContentAsString(nameof(PostCode), string.Empty);
         reader.ReadEndElement();
      }

      public void WriteXml(XmlWriter writer)
      {
         writer.WriteElementString(nameof(Street), Street);
         writer.WriteElementString(nameof(PostCode), PostCode);
      }
   }

   public class UsAddress : Address
   {
   }

   public class AuAddress : Address
   {
   }
}