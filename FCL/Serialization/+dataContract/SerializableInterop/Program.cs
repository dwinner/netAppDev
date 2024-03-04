using System.Runtime.Serialization;
using System.Xml.Linq;

namespace SerializableInterop;

internal class Program
{
   private static void Main(string[] args)
   {
      var person = new Person
      {
         Name = "Stacey",
         MailingAddress = new Address { Postcode = "6000", Street = "Mary St" }
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
   [DataMember] public Address MailingAddress;
   [DataMember] public string Name;
}

[Serializable]
public class Address
{
   public string Street, Postcode;
}