/**
 * Типы, допускающие значения null
 */

using System;

namespace _04_NullableTypes
{
   class Program
   {
      static void Main()
      {
         var emp = new Employee("Vasya", "Pupkin") { Ssn = 1234567890 };
         Console.WriteLine("{0} {1}", emp.FirstName, emp.LastName);
         if (emp.TerminationDate.HasValue)
         {
            Console.WriteLine("Start Date: {0}", emp.TerminationDate);
         }
         long tempSsn = emp.Ssn ?? Employee.DefaultSsn;
         Console.WriteLine("SSN: {0}", tempSsn);

         Console.ReadKey();
      }
   }

   public class Employee
   {
      public const int DefaultSsn = -1;

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public Nullable<DateTime> TerminationDate { get; set; }

      public long? Ssn { get; set; }   // Сокращенная нотация

      public Employee(string firstName, string lastName)
      {
         FirstName = firstName;
         LastName = lastName;
         TerminationDate = null;
         Ssn = default(Nullable<long>);
      }
   }
}
