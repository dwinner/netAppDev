/**
 * Сериализация объекта в XML и десериализация
 */

using System;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace XmlSerializationDemo
{
   public class Person
   {
      public string FirstName { get; set; }
      public char MiddleInitial { get; set; }
      public string LastName { get; set; }
      public DateTime BirthDate { get; set; }
      public double HighschoolGpa { get; set; }
      public Address Address { get; set; }

      // NOTE: Чтобы тип можно было сериализовать в XML, у него должен быть конструктор без параметров
      public Person() { }

      public Person(string firstName,
                    char middleInitial,
                    string lastName,
                    DateTime birthDate,
                    double highSchoolGpa,
                    Address address)
      {
         FirstName = firstName;
         MiddleInitial = middleInitial;
         LastName = lastName;
         BirthDate = birthDate;
         HighschoolGpa = highSchoolGpa;
         Address = address;
      }

      public override string ToString()
      {
         return string.Format("{0} {1}. {2}, DOB:{3}, GPA: {4}{5}{6}", FirstName, MiddleInitial, LastName,
            BirthDate.ToShortDateString(), HighschoolGpa, Environment.NewLine, Address);
      }
   }

   // Увы, здесь перечислены далеко не все 50 штатов :)
   public enum StateAbbreviation { Ri, Va, Sc, Ca, Tx, Ut, Wa };

   public class Address
   {
      public string AddressLine1 { get; set; }
      public string AddressLine2 { get; set; }
      public string City { get; set; }
      public StateAbbreviation State { get; set; }
      public string ZipCode { get; set; }

      public Address() { }

      public Address(string addressLine1,
                     string addressLine2,
                     string city,
                     StateAbbreviation state,
                     string zipCode)
      {
         AddressLine1 = addressLine1;
         AddressLine2 = addressLine2;
         City = city;
         State = state;
         ZipCode = zipCode;
      }

      public override string ToString()
      {
         return string.Format("{0}{1}{2}{1}{3}, {4} {5}", AddressLine1, Environment.NewLine, AddressLine2, City, State,
            ZipCode);
      }
   }

   class Program
   {
      static void Main()
      {
         var person = new Person("John", 'Q', "Public", new DateTime(1776, 7, 4), 3.5,
            new Address("1234 Cherry Lane", null, "Smalltown", StateAbbreviation.Va, "10000"));
         Console.WriteLine("Before serialize:{0}{1}",
            Environment.NewLine, person);

         var serializer = new XmlSerializer(typeof(Person));
         // Для демонстрационных целей выполняем сериализацию в строку
         var stringBuilder = new StringBuilder();
         using (var stringWriter = new StringWriter(stringBuilder))
         {
            serializer.Serialize(stringWriter, person);
            Console.WriteLine("{0}XML:{0}{1}{0}", Environment.NewLine, stringBuilder);
         }

         using (var stringReader = new StringReader(stringBuilder.ToString()))
         {
            var newPerson = serializer.Deserialize(stringReader) as Person;
            Console.WriteLine("After deserialize:{0}{1}", Environment.NewLine, newPerson);
         }
         Console.ReadKey();
      }
   }
}
