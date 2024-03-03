using System.Xml.Serialization;

namespace RenamingElements;

internal static class Program
{
   private static void Main()
   {
      var p = new Person { Name = "Stacey" };
      p.Addresses.Add(new Address { Street = "My Street", PostCode = "1234" });
      p.Addresses.Add(new Address { Street = "My Way", PostCode = "2345" });

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
   [XmlArray("PreviousAddresses")] [XmlArrayItem("Location")]
   public List<Address> Addresses = new();

   public string Name;
}

public class Address
{
   public string Street, PostCode;
}