#nullable disable
using System.Runtime.Serialization;
using System.Xml.Linq;
using SerializingCollections.SerialTest;

namespace SerializingCollections
{
   internal static class Program
   {
      private static void Main()
      {
         var p = new Person
         {
            Name = "Li",
            Addresses =
            [
               new() { Postcode = "1000", Street = "One" },
               new() { Postcode = "2000", Street = "Two" }
            ]
         };

         var ds = new DataContractSerializer(typeof(Person));

         using (Stream s = File.Create("person.xml"))
         {
            ds.WriteObject(s, p); // Serialize 
         }

         var xDocument = XDocument.Load("person.xml");
         Console.WriteLine(xDocument);
      }
   }

   namespace SerialTest
   {
      [DataContract]
      public class Person
      {
         [DataMember] public List<Address> Addresses;
         [DataMember] public string Name;
      }

      [DataContract]
      public class Address
      {
         [DataMember] public string Street, Postcode;
      }
   }
}