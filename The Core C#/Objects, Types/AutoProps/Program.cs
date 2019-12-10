using System;

namespace AutoProps
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** Fun with Automatic Properties *****\n");

         // Make a car.
         var c = new Car {PetName = "Frank", Speed = 55, Color = "Red"};
         c.DisplayStats();

         // Put car in the garage.
         var g = new Garage();
         g.MyAuto = c;
         Console.WriteLine("Number of Cars in garage: {0}", g.NumberOfCars);
         Console.WriteLine("Your car is named: {0}", g.MyAuto.PetName);

         Console.ReadLine();
      }
   }
}