using System;

namespace PrimAndProperCarEvents
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Events *****\n");
         Car c1 = new Car("SlugBug", 100, 10);

         // Регистрируем обработчики событий
         c1.AboutToBlow += CarAboutToBlow;
         c1.Exploded += CarExploded;

         Console.WriteLine("***** Speeding up *****");
         for (int i = 0; i < 6; i++)
            c1.Accelerate(20);

         c1.Exploded -= CarExploded;

         Console.WriteLine("\n***** Speeding up *****");
         for (int i = 0; i < 6; i++)
            c1.Accelerate(20);

         Console.ReadLine();
      }

      #region Цели для событий
      public static void CarAboutToBlow(object sender, CarEventArgs e)
      {
         if (sender is Car)
         {
            Car c = (Car)sender;
            Console.WriteLine("Critical Message from {0}: {1}", c.PetName, e.msg);
         }
      }

      public static void CarExploded(object sender, CarEventArgs e)
      {
         Console.WriteLine(e.msg);
      }
      #endregion
   }
}
