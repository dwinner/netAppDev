using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace InteropWithIXmlSerializable;

internal static class Program
{
   private static void Main()
   {
      var p = new Person
      {
         Name = "Stacey",
         HomeAddress = new Address { Street = "My Street", PostCode = "90210" }
      };

      SerializePerson(p, "person.xml");
      var xmlDump = File.ReadAllText("person.xml");
      Console.WriteLine(xmlDump);
   }

   private static void SerializePerson(Person p, string path)
   {
      var xs = new XmlSerializer(typeof(Person));
      using Stream s = File.Create(path);
      xs.Serialize(s, p);
   }
}

public class Person
{
   public Address HomeAddress;
   public string Name;
}

public class Address : IXmlSerializable
{
   public string Street, PostCode;

   public XmlSchema GetSchema() => null;

   public void ReadXml(XmlReader reader)
   {
      reader.ReadStartElement();
      Street = reader.ReadElementContentAsString("Street", "");
      PostCode = reader.ReadElementContentAsString("PostCode", "");
      reader.ReadEndElement();
   }

   public void WriteXml(XmlWriter writer)
   {
      writer.WriteElementString("Street", Street);
      writer.WriteElementString("PostCode", PostCode);
   }
}