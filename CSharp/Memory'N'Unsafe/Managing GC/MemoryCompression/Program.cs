/**
 * Методы сжатия памяти
 */

using System;
using System.Runtime.InteropServices;

namespace _10_MemoryCompression
{
   internal static class Program
   {
      private static void Main()
      {
         MemoryPressureDemo(0); // 0 вызывает нечастую сборку мусора
         MemoryPressureDemo(10 * 1024 * 1024); // 10 Mb вызывают частую сборку мусора
         HandleCollectorDemo();
      }

      private static void HandleCollectorDemo()
      {
         Console.WriteLine();
         Console.WriteLine("Handle collection demo");
         for (var i = 0; i < 10; i++)
         {
            // ReSharper disable ObjectCreationAsStatement
            new LimitedResource();
            // ReSharper restore ObjectCreationAsStatement
         }

         // В демонстрационных целях очищаем все
         GC.Collect();
         GC.WaitForPendingFinalizers();
      }

      private static void MemoryPressureDemo(int size)
      {
         Console.WriteLine();
         Console.WriteLine("Memory pressure demo, size={0}", size);
         // Создание набора объектов с указанием их логического размера
         for (var i = 0; i < 15; i++)
         {
            // ReSharper disable ObjectCreationAsStatement
            new BigNativeResource(size);
            // ReSharper restore ObjectCreationAsStatement
         }

         // В демонстрационных целях очищаем все
         GC.Collect();
         GC.WaitForPendingFinalizers();
      }

      private sealed class LimitedResource
      {
         // Создаем объект HandleCollector и передаем ему
         // указание перейти к очистке, когда в куче появится два или более
         // объекта LimitedResource
         private static readonly HandleCollector HandleCollector = new HandleCollector(nameof(LimitedResource), 2);

         public LimitedResource()
         {
            // Сообщаем HandleCollector, что в кучу добавлен еще один объект LimitedResource
            HandleCollector.Add();
            Console.WriteLine("Limited resource created. Count={0}", HandleCollector.Count);
         }

         ~LimitedResource()
         {
            // Сообщаем HandleCollector, что один объект LimitedResource удален из кучи
            HandleCollector.Remove();
            Console.WriteLine("Limited resource destroy. Count={0}", HandleCollector.Count);
         }
      }

      private sealed class BigNativeResource
      {
         private readonly int _size;

         public BigNativeResource(int size)
         {
            _size = size;
            if (_size > 0)
            {
               // Пусть сборщик думает, что объект занимает больше памяти
               GC.AddMemoryPressure(_size);
            }
         }

         ~BigNativeResource()
         {
            if (_size > 0)
            {
               // Пусть сборщик думает, что объект освободил больше памяти
               GC.RemoveMemoryPressure(_size);
            }
         }
      }
   }
}