using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using SerialTest;

namespace ElementNames
{
   internal static class Program
   {
      private static void Main()
      {
         var p = new Person { Name = "Stacey", Age = 30 };
         var ds = new DataContractSerializer(typeof(Person));

         using (var w = XmlWriter.Create("person.xml"))
         {
            ds.WriteObject(w, p);
         }

         var xDocument = XDocument.Load("person.xml");
         Console.WriteLine(xDocument);
      }
   }
}

namespace SerialTest
{
   [DataContract(Name = "Candidate")]
   public class Person
   {
      [DataMember(Name = "ClaimedAge")] public int Age;
      [DataMember(Name = "FirstName")] public string Name;
   }
}