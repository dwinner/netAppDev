/**
 * Serializing subclasses via WCF's DataContract attributes
 */

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DataContract.SerialSublasses
{
   internal static class Program
   {
      private static void Main()
      {
         var person = new Person {Name = "Stacey", Age = 30};
         var student = new Student {Name = "Stacey", Age = 30};
         var teacher = new Teacher {Name = "Stacey", Age = 30};

         var clonePerson = DeepClone(person);
         var cloneStudent = DeepClone(student);
         var cloneTeacher = DeepClone(teacher);

         Console.WriteLine(clonePerson);
         Console.WriteLine(cloneStudent);
         Console.WriteLine(cloneTeacher);

         // Alternative to KnownType on [DataContract]
         //DataContractSerializer serializer=new DataContractSerializer(typeof(Person), new []{typeof(Student), typeof(Teacher)});
      }

      private static T DeepClone<T>(T genericType)
      {
         var serializer = new DataContractSerializer(typeof(T));
         using (var stream = new MemoryStream())
         {
            serializer.WriteObject(stream, genericType);
            stream.Position = 0;
            return (T) serializer.ReadObject(stream);
         }
      }
   }

   [DataContract]
   [KnownType(typeof(UsAddress))]
   public class Address
   {
      [DataMember(Order = 0)]
      public string Street { get; set; }

      [DataMember(Order = 1)]
      public string PostCode { get; set; }

      public override string ToString() => $"{nameof(Street)}: {Street}, {nameof(PostCode)}: {PostCode}";
   }

   [DataContract]
   public class UsAddress : Address
   {
   }

   [DataContract]
   [KnownType(typeof(Student))]
   [KnownType(typeof(Teacher))]
   public class Person : IExtensibleDataObject
   {
      [DataMember(Order = 0, EmitDefaultValue = false)]
      public string Name { get; set; }

      [DataMember(Order = 1, EmitDefaultValue = false)]
      public int Age { get; set; }

      [DataMember(Order = 2)]
      public Address HomeAddress { get; set; }

      public ExtensionDataObject ExtensionData { get; set; }

      public override string ToString() =>
         $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}, {nameof(HomeAddress)}: {HomeAddress}";
   }

   [DataContract]
   public class Student : Person
   {
   }

   [DataContract]
   public class Teacher : Person
   {
   }
}