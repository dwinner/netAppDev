#nullable disable
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ObjRefsKnownTypes;

internal static class Program
{
   private static void Main()
   {
      var p = new Person
      {
         Name = "John", Age = 30,
         HomeAddress = new USAddress
         {
            Street = "Fawcett St",
            Postcode = "02138"
         }
      };

      var ds = new DataContractSerializer(typeof(Person));

      using (Stream s = File.Create("person.xml"))
      {
         ds.WriteObject(s, p); // Serialize
      }

      var xDoc = XDocument.Load("person.xml");
      Console.WriteLine(xDoc);
   }
}

[DataContract]
public class Person
{
   [DataMember] public int Age;
   [DataMember] public Address HomeAddress;
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