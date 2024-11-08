﻿using System.Xml.Serialization;

namespace SubclassedElements_NoOuter;

internal static class Program
{
   private static void Main()
   {
      var p = new Person { Name = "Stacey" };
      p.Addresses.Add(new USAddress { Street = "My Street", PostCode = "90210" });
      p.Addresses.Add(new AUAddress { Street = "My Way", PostCode = "6000" });

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

public class Address
{
   public string Street, PostCode;
}

public class USAddress : Address;

public class AUAddress : Address;

public class Person
{
   [XmlElement("Address", typeof(Address))]
   [XmlElement("AUAddress", typeof(AUAddress))]
   [XmlElement("USAddress", typeof(USAddress))]
   public List<Address> Addresses = new();

   public string Name;
}