/**
 * Встроенная поддержка шаблона подписчика/потребителя
 */

using System;

namespace Observer.OutOfTheBox
{
   internal static class Program
   {
      private static void Main()
      {
         var generator = new DataGenerator();

         var observer1 = new DataObserver("O1");
         var observer2 = new DataObserver("O2");

         generator.Subscribe(observer1);
         generator.Subscribe(observer2);

         generator.Run();

         Console.WriteLine("Press any key to exit...");
         Console.ReadKey();
      }
   }
}