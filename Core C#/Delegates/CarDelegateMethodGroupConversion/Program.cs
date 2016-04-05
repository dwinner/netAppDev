using System;

namespace CarDelegateMethodGroupConversion
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Method Group Conversion *****\n");
         Car c1 = new Car();

         // Регистрируем имя метода.
         c1.RegisterWithCarEngine(CallMeHere);

         Console.WriteLine("***** Speeding up *****");
         for (int i = 0; i < 6; i++)
            c1.Accelerate(20);

         // Отменяем регистрацию метода.
         c1.UnRegisterWithCarEngine(CallMeHere);

         // Уведомлений не поступает!
         for (int i = 0; i < 6; i++)
            c1.Accelerate(20);

         Console.ReadLine();
      }

      static void CallMeHere(string msg)
      {
         Console.WriteLine("=> Message from Car: {0}", msg);
      }
   }
}
