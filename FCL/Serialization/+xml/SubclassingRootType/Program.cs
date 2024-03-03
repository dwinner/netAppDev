using System.Xml.Serialization;

namespace SubclassingRootType;

internal static class Program
{
   private static void Main()
   {
      var p = new Student { Name = "Stacey" };
      SerializePerson(p, "person.xml");
      var xmlDump = File.ReadAllText("person.xml");
      Console.WriteLine(xmlDump);
   }

   private static void SerializePerson(Person p, string path)
   {
      var xs = new XmlSerializer(typeof(Person));
      using Stream s = File.Create(path);
      xs.Serialize(s, p);
   }
}

[XmlInclude(typeof(Student))]
[XmlInclude(typeof(Teacher))]
public class Person
{
   public string Name;
}

public class Student : Person;

public class Teacher : Person;