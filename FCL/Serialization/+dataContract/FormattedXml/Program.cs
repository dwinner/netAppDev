using System.Runtime.Serialization;
using System.Xml;

namespace FormattedXml;

internal class Program
{
   private static void Main()
   {
      var p = new Person { Name = "Stacey", Age = 30 };
      var ds = new DataContractSerializer(typeof(Person));

      var settings = new XmlWriterSettings { Indent = true };
      using (var w = XmlWriter.Create("person.xml", settings))
      {
         ds.WriteObject(w, p);
      }

      var xmlDump = File.ReadAllText("person.xml");
      Console.WriteLine(xmlDump);
   }

   [DataContract]
   public class Person
   {
      [DataMember] public int Age;
      [DataMember] public string Name;
   }
}