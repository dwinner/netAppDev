using System.Xml.Serialization;

namespace SubclassingChildObjects1;

internal static class Program
{
   private static void Main()
   {
      var p = new Person
      {
         Name = "Stacey",
         HomeAddress =
         {
            Street = "Odo St",
            PostCode = "6020"
         }
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

[XmlInclude(typeof(AUAddress))]
[XmlInclude(typeof(USAddress))]
public class Address
{
   public string Street, PostCode;
}

public class USAddress : Address
{
}

public class AUAddress : Address
{
}

public class Person
{
   public Address HomeAddress = new USAddress();
   public string Name;
}