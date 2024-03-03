#nullable disable
using System.Xml.Serialization;

namespace AttrBased_Intro;

internal static class Program
{
   private static void Main()
   {
      var p = new Person
      {
         Name = "Stacey",
         Age = 30
      };

      var xs = new XmlSerializer(typeof(Person));
      using (Stream s = File.Create("person.xml"))
      {
         xs.Serialize(s, p);
      }

      Person p2;
      using (Stream s = File.OpenRead("person.xml"))
      {
         p2 = (Person)xs.Deserialize(s);
      }

      Console.WriteLine(p2.Name + " " + p2.Age); // Stacey 30

      var restored = File.ReadAllText("person.xml");
      Console.WriteLine(restored);
   }
}

public class Person
{
   public int Age;
   public string Name;
}