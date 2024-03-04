#nullable disable
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ObjectReferences;

internal static class Program
{
   private static void Main()
   {
      var person = new Person
      {
         Name = "Stacey",
         Age = 30,
         HomeAddress = new Address { Postcode = "6000", Street = "Mary St" }
      };

      var ds = new DataContractSerializer(typeof(Person));

      using (Stream s = File.Create("person.xml"))
      {
         ds.WriteObject(s, person); // Serialize
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
public class Address
{
   [DataMember] public string Street, Postcode;
}