using System;

namespace CustomEnumeratorWithYield
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with the Yield Keyword *****\n");
         var carLot = new Garage();

         // Get items using GetEnumerator().
         foreach (Car c in carLot)
         {
            Console.WriteLine("{0} is going {1} MPH",
               c.PetName,
               c.CurrentSpeed);
         }

         Console.WriteLine();

         // Get items (in reverse!) using named iterator.
         foreach (Car c in carLot.GetTheCars(true))
         {
            Console.WriteLine("{0} is going {1} MPH",
               c.PetName,
               c.CurrentSpeed);
         }

         Console.ReadLine();
      }
   }
}