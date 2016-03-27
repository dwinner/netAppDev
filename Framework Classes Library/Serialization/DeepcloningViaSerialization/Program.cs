// Создание глубокой копии объекта через механизмы сериализации

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepcloningViaSerialization
{
   internal static class Program
   {
      private static void Main()
      {
         var employee = new Employee
         {
            Firstname = "Denis",
            LastName = "Vinevcev",
            Salary = 60000
         };

         var deepClone = DeepClone(employee);
         employee.Salary = 90000;
         Console.WriteLine(deepClone);
      }

      private static T DeepClone<T>(T original)
      {
         using (var stream = new MemoryStream())
         {
            var formatter = new BinaryFormatter
            {
               Context = new StreamingContext(StreamingContextStates.Clone)
            };
            formatter.Serialize(stream, original);
            stream.Position = 0;
            return (T) formatter.Deserialize(stream);
         }
      }
   }

   [Serializable]
   internal class Employee
   {
      public string Firstname { get; set; }
      public string LastName { get; set; }
      public double Salary { get; set; }

      public override string ToString()
      {
         return $"Firstname: {Firstname}, LastName: {LastName}, Salary: {Salary}";
      }
   }
}