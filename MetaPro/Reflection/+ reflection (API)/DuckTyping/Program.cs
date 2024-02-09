using System;

namespace DuckTyping
{
   internal static class Program
   {
      private static void Main()
      {
         var golfer = new Golfer();
         var returnValue = golfer.Call("Drive", "Reflection");
         Console.WriteLine(returnValue);

         var raceCar = new RaceCarDriver();
         returnValue = raceCar.Call("Drive", "Reflection");

         Console.WriteLine(returnValue);
      }
   }
}