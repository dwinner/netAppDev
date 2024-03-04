#nullable disable
using System.Runtime.Serialization;
using System.Xml.Linq;
using SerialTest;

namespace DCGettingStarted
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

         Person p2;
         using (Stream s = File.OpenRead("person.xml"))
         {
            p2 = (Person)ds.ReadObject(s); // Deserialize
         }

         Console.WriteLine(p2);

         var xmlContent = File.ReadAllText("person.xml");
         Console.WriteLine(xmlContent);

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
      [DataMember] public int Age;
      [DataMember] public string Name;

      public override string ToString() => $"{nameof(Age)}: {Age}, {nameof(Name)}: {Name}";
   }
}