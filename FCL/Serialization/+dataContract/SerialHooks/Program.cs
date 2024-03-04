using System.Runtime.Serialization;
using System.Xml.Linq;
using JetBrains.Annotations;
using SerialTest;

namespace SerialHooks
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
         var xDoc = XDocument.Load("person.xml");
      }
   }
}

namespace SerialTest
{
   [DataContract]
   public class Person
   {
      [DataMember] public int Age;
      [DataMember] public string? Name;

      [OnSerializing]
      [UsedImplicitly]
      private void PrepareForSerialization(StreamingContext sc)
      {
         Console.WriteLine("OnSerializing");
      }

      [OnDeserialized]
      [UsedImplicitly]
      private void CompleteDeserialization(StreamingContext streamingCtx)
      {
         Console.WriteLine("OnDeserialized");
      }

      public override string ToString() => $"{nameof(Age)}: {Age}, {nameof(Name)}: {Name}";
   }
}