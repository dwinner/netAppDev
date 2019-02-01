// Фиксация адресов

using System;

namespace _06_FixingObjects
{
   internal static class Program
   {
      private static void Main()
      {
         Go();
      }

      private static unsafe void Go()
      {
         // Выделение места под объекты, которые немедленно превращаются в мусор
         for (var i = 0; i < 10000; i++)
         {
            new object();
         }

         IntPtr originalMemoryAddress;
         var bytes = new byte[1000]; // располагаем этот массив после мусорных объектов
         fixed (byte* pbytes = bytes) // получаем адрес в памяти массива byte[]
         {
            originalMemoryAddress = (IntPtr)pbytes;
         }

         // Принудительная сборка мусора. Мусор исчезает, позволяя сжать массив byte[]
         GC.Collect();

         // Повторное получение адреса массива byte[] в памяти и сравнение двух адресов
         fixed (byte* pbytes = bytes)
         {
            Console.WriteLine("The byte[] did{0} move during the GC",
               (originalMemoryAddress == (IntPtr)pbytes) ? " not" : null);
         }
      }
   }
}