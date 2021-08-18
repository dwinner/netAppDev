using System;

namespace CarDelegate
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Delegates as event enablers *****\n");

         Car c1 = new Car("SlugBug", 100, 10);
         c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
         Car.CarEngineHandler handler2 = new Car.CarEngineHandler(OnCarEngineEvent2);
         c1.RegisterWithCarEngine(handler2);

         // Разгоняемся (порождая события).
         Console.WriteLine("***** Speeding up *****");
         for (int i = 0; i < 6; i++)
            c1.Accelerate(20);

         // Отключаеи второй обработчик 
         c1.UnRegisterWithCarEngine(handler2);

         Console.WriteLine("***** Speeding up *****");
         for (int i = 0; i < 6; i++)
            c1.Accelerate(20);

         Console.ReadLine();
      }

      #region Методы для делегата
      // We now have TWO methods that will be called by the Car
      // when sending notifications. 
      public static void OnCarEngineEvent(string msg)
      {
         Console.WriteLine("\n***** Message From Car Object *****");
         Console.WriteLine("=> {0}", msg);
         Console.WriteLine("***********************************\n");
      }

      public static void OnCarEngineEvent2(string msg)
      {
         Console.WriteLine("=> {0}", msg.ToUpper());
      }
      #endregion
   }
}
