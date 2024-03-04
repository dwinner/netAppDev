using System.Runtime.Serialization;
using System.Xml.Linq;
using SerialTest;

namespace MemberOrdering
{
   internal static class Program
   {
      private static void Main()
      {
         var p = new Person { Name = "Stacey", Age = 30 };
         var ds = new DataContractSerializer(typeof(Person));
         using (Stream s = File.Create("person.xml"))
         {
            ds.WriteObject(s, p); // Serialize 
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
      [DataMember(Order = 1)] public int Age;
      [DataMember(Order = 0)] public string Name;
   }
}