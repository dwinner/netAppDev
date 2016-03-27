// Недожившый объект

using System;
using System.Threading;

namespace _01_NotLivedObject
{
   internal static class Program
   {
      private static void Main()
      {
         // Создание объекта Timer, вызывающего метод TimerCallback каждые 2000 миллисекунд
         var timer = new Timer(TimeCallback, null, 0, 2000);

         Console.ReadLine();
      }

      private static void TimeCallback(object state)
      {
         Console.WriteLine("In timer callback: {0}", DateTime.Now);
         GC.Collect(); // NOTE: принудительная сборка мусора
      }
   }
}