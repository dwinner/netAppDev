using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Linq;
using SerialTest;

namespace CustomizingCollections
{
   internal static class Program
   {
      private static void Main()
      {
         var person = new Person
         {
            Name = "Li",
            Addresses =
            [
               new Address { Postcode = "1000", Street = "One" },
               new UsAddress { Postcode = "2000", Street = "Two" }
            ]
         };

         var serializer = new DataContractSerializer(typeof(Person));
         using (Stream oStream = File.Create("person.xml"))
         {
            serializer.WriteObject(oStream, person); // Serialize 
         }

         var xDocument = XDocument.Load("person.xml");
         Console.WriteLine(xDocument);
      }
   }
}

namespace SerialTest
{
   [DataContract]
   public class Person
   {
      [DataMember] public AddressList? Addresses;
      [DataMember] public string? Name;
   }

   [CollectionDataContract(ItemName = "Residence")]
   public class AddressList : Collection<Address>;

   [DataContract]
   [KnownType(typeof(UsAddress))]
   public class Address
   {
      [DataMember] public string? Postcode;
      [DataMember] public string? Street;
   }

   public class UsAddress : Address;
}