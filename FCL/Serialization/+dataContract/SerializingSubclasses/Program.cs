#nullable disable

using System.Runtime.Serialization;
using System.Xml.Linq;

namespace SerializingSubclasses;

internal static class Program
{
   private static void Main()
   {
      var person = new Person { Name = "Stacey", Age = 30 };
      var student = new Student { Name = "Stacey", Age = 30 };
      var teacher = new Teacher { Name = "Stacey", Age = 30 };

      try
      {
         var p2 = DeepClone(person); // OK
         var s2 = (Student)DeepClone(student); // SerializationException
         var t2 = (Teacher)DeepClone(teacher); // SerializationException

         Console.WriteLine(p2);
         Console.WriteLine(s2);
         Console.WriteLine(t2);
      }
      catch (SerializationException serializationException)
      {
         Console.WriteLine(serializationException.Message);
      }
   }

   private static Person DeepClone(Person p)
   {
      var ds = new DataContractSerializer(typeof(Person),
         new[] { typeof(Student), typeof(Teacher) });

      var stream = new MemoryStream();
      ds.WriteObject(stream, p);
      stream.Position = 0;
      DumpXmlStream(stream);
      return (Person)ds.ReadObject(stream);
   }

   private static void DumpXmlStream(Stream s)
   {
      var xDoc = XDocument.Load(s);
      s.Position = 0;
      Console.WriteLine(xDoc);
   }
}

[DataContract]
public class Person
{
   [DataMember] public int Age;
   [DataMember] public string Name;
}

[DataContract]
public class Student : Person
{
}

[DataContract]
public class Teacher : Person
{
}