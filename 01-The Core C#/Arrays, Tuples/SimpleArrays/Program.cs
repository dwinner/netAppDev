/**
 * Простые массивы
 */

using System;

namespace SimpleArrays
{
   static class Program
   {
      static void Main()
      {
         var myPersons = new Person[2];

         myPersons[0] = new Person { FirstName = "Ayrton", LastName = "Senna" };
         myPersons[1] = new Person { FirstName = "Michael", LastName = "Schumacher" };

         Person[] myPersons2 =
         {
            new Person {FirstName = "Ayrton", LastName = "Senna"},
            new Person {FirstName = "Michael", LastName = "Schumacher"}
         };

         foreach (var person in myPersons2)
         {
            Console.WriteLine(person);
         }

         foreach (var myPerson in myPersons)
         {
            Console.WriteLine(myPerson);
         }
      }
   }
}
