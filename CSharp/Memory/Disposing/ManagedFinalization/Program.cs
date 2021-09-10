// Управляемая финализация

using System;

namespace _04_ManagedFinalization
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         // После создания объекта GCBeep каждый раз, когда
         // начинается сборка мусора, подается звуковой сигнал
         new GcBeep();

         // Создать много 100-байтовых объектов
         for (var i = 0; i < 10000; i++)
         {
            Console.WriteLine(i);
            var b = new byte[100];
         }
      }
   }

   internal sealed class GcBeep
   {
      ~GcBeep()
      {
         Console.Beep(); // Идет финализация - дать сигнал

         // Если домен приложения не выгружается, а процесс не завершается,
         // создать новый объект, финализация которого произойдет
         // в ходе следующей сборки мусора
         if (!AppDomain.CurrentDomain.IsFinalizingForUnload() && !Environment.HasShutdownStarted)
         {
            new GcBeep();
         }
      }
   }
}