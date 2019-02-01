using System;

namespace DelegateCovariance
{
   class Program
   {
      public delegate Car ObtainVehicalDelegate();

      public static Car GetBasicCar()
      {
         return new Car();
      }

      public static SportsCar GetSportsCar()
      {
         return new SportsCar();
      }

      static void Main(string[] args)
      {
         Console.WriteLine("***** Delegate Covariance *****\n");
         ObtainVehicalDelegate targetA = new ObtainVehicalDelegate(GetBasicCar);
         Car c = targetA();
         Console.WriteLine("Obtained a {0}", c);

         // Делегаты ковариантны.
         ObtainVehicalDelegate targetB = new ObtainVehicalDelegate(GetSportsCar);
         SportsCar sc = (SportsCar)targetB();
         Console.WriteLine("Obtained a {0}", sc);
         Console.ReadLine();
      }
   }

}
