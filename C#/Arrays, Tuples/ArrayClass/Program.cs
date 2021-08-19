/**
 * Класс Array
 */

using System;

namespace ArrayClass
{
   static class Program
   {
      static void Main()
      {
         Array intArray = new int[5];  // Array.CreateInstance(typeof (int), 5)
         for (int i = 0; i < 5; i++)
         {
            intArray.SetValue(33, i);
         }

         for (int i = 0; i < intArray.Length; i++)
         {
            Console.WriteLine(intArray.GetValue(i));
         }

         int[] lengths = { 2, 3 };
         int[] lowerBounds = { 1, 10 };
         Array racers = Array.CreateInstance(typeof(Person), lengths, lowerBounds);
         racers.SetValue(new Person { FirstName = "Alain", LastName = "Prost" }, 1, 10);
         racers.SetValue(new Person { FirstName = "Emerson", LastName = "Fittipaldi" }, 1, 11);
         racers.SetValue(new Person { FirstName = "Ayrton", LastName = "Senna" }, 1, 12);
         racers.SetValue(new Person { FirstName = "Ralf", LastName = "Schumacher" }, 2, 10);
         racers.SetValue(new Person { FirstName = "Fernando", LastName = "Alonso" }, 2, 11);
         racers.SetValue(new Person { FirstName = "Jenson", LastName = "Button" }, 2, 12);         

         var racers2 = (Person[,]) racers;
         Person first = racers2[1, 10];
         Person last = racers2[2, 12];

         Console.WriteLine(first);
         Console.WriteLine(last);
      }

      public static T[] CreateArray<T>(T type, int length)
      {
         return new T[length];
      }
   }
}
