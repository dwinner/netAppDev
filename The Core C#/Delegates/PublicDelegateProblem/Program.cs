using System;

namespace PublicDelegateProblem
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Agh!  No Encapsulation! *****\n");
         // Make a Car.
         Car myCar = new Car();
         // We have direct access to the delegate!
         myCar.listOfHandlers = new Car.CarEngineHandler(CallWhenExploded);
         myCar.Accelerate(10);

         // We can now assign to a whole new object...
         // confusing at best.
         myCar.listOfHandlers = new Car.CarEngineHandler(CallHereToo);
         myCar.Accelerate(10);

         // The caller can also directly invoke the delegate!
         myCar.listOfHandlers.Invoke("hee, hee, hee...");
         Console.ReadLine();
      }

      static void CallWhenExploded(string msg)
      { Console.WriteLine(msg); }

      static void CallHereToo(string msg)
      { Console.WriteLine(msg); }
   }
}
