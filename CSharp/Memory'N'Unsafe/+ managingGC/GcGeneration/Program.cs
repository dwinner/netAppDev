// Работа поколений сборщика мусора

using System;
using static System.Console;

namespace _12_GcGeneration
{
   internal static class Program
   {
      private static void Main()
      {
         WriteLine($"Maximum generations: {GC.MaxGeneration}");

         // Создание нового объекта в куче
         object o = new GenObj();

         // Поскольку этот объект создан недавно, он помещается в поколение 0
         WriteLine($"Gen: {GC.GetGeneration(0)}");

         // Сборка мусора переводит объект в следующее поколение
         GC.Collect();
         WriteLine($"Gen: {GC.GetGeneration(o)}"); // 1

         GC.Collect();
         WriteLine($"Gen: {GC.GetGeneration(o)}"); // 2

         GC.Collect();
         WriteLine($"Gen: {GC.GetGeneration(o)}"); // 2: максимальное значение

         o = null;   // Уничтожаем жесткую ссылку на объект

         WriteLine("Collecting Gen 0");
         GC.Collect(0); // Сборка мусора в поколении 0
         GC.WaitForPendingFinalizers();   // Методы финализации НЕ вызываются

         WriteLine("Collecting Gen 0 and 1");
         GC.Collect(1); // Сборка мусора в поколенияз 0 и 1
         GC.WaitForPendingFinalizers();   // Методы финализации НЕ вызываются

         WriteLine("Collecting Gen 0, 1 and 2");
         GC.Collect(2); // Тотальная сборка мусора
         GC.WaitForPendingFinalizers();   // Вызываются методы финализации
      }
   }

   internal sealed class GenObj
   {
      ~GenObj()
      {
         WriteLine("In Finalize method");
      }
   }
}