#nullable disable
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace PreservingObjRefs;

internal static class Program
{
   private static void Main()
   {
      var p = new Person { Name = "Stacey", Age = 30 };
      p.HomeAddress = new Address { Street = "Odo St", Postcode = "6020" };
      p.WorkAddress = p.HomeAddress;

      var settings = new DataContractSerializerSettings { PreserveObjectReferences = true };
      var ds = new DataContractSerializer(typeof(Person), settings);

      using (Stream s = File.Create("person.xml"))
      {
         ds.WriteObject(s, p); // Serialize
      }

      var xDocument = XDocument.Load("person.xml");
      Console.WriteLine(xDocument);

      Person p2;
      using (Stream s = File.OpenRead("person.xml"))
      {
         p2 = (Person)ds.ReadObject(s); // Deserialize
      }

      Console.WriteLine(p2.HomeAddress == p2.WorkAddress);
   }
}

[DataContract]
public class Person
{
   [DataMember] public int Age;
   [DataMember] public Address HomeAddress, WorkAddress;
   [DataMember] public string Name;
}

[DataContract]
[KnownType(typeof(USAddress))]
public class Address
{
   [DataMember] public string Street, Postcode;
}

[DataContract]
public class USAddress : Address
{
}