using System;

namespace AutoProps
{
   internal class Car
   {
      // Automatic properties!
      public string PetName { get; set; }

      public int Speed { get; protected internal set; }

      public string Color { get; protected internal set; }

      public void DisplayStats()
      {
         Console.WriteLine("Car Name: {0}", PetName);
         Console.WriteLine("Speed: {0}", Speed);
         Console.WriteLine("Color: {0}", Color);
      }
   }
}