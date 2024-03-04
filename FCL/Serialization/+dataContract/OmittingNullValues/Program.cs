using System.Runtime.Serialization;
using System.Xml.Linq;
using OmittingNullValues.SerialTest;

namespace OmittingNullValues
{
   internal static class Program
   {
      private static void Main()
      {
         var p = new Person { Age = 30 };
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
         [DataMember] public int Age;
         [DataMember(EmitDefaultValue = false)] public string Name;
      }
   }
}