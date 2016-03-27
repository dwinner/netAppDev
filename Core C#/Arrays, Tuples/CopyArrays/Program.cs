/**
 * Копирование массивов
 */

using System;
using ArrayClass;

namespace CopyArrays
{
   static class Program
   {
      static void Main()
      {
         Person[] beatles =
         {
            new Person {FirstName = "John", LastName = "Lennon"},
            new Person {FirstName = "Paul", LastName = "McCartney"},
         };
         var beatlesClone = beatles.Clone() as Person[];
         if (beatlesClone != null)
            foreach (var person in beatlesClone)
            {
               Console.WriteLine(person);
            }
      }
   }
}
