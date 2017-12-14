// Отражение атрибутов с использованием раннего связывания.

using System;
using AttributedCarLibrary;

namespace VehicleDescriptionAttributeReader
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** Value of VehicleDescriptionAttribute *****\n");
         ReflectOnAttributesUsingEarlyBinding();
         Console.ReadLine();
      }

      private static void ReflectOnAttributesUsingEarlyBinding()
      {
         // Получение типа, представляющего Winnebago.
         var t = typeof (Winnebago);

         // Получение всех атрибутов Winnebago.
         var customAtts = t.GetCustomAttributes(false);

         // Вывод описания.
         foreach (VehicleDescriptionAttribute v in customAtts)
            Console.WriteLine("-> {0}\n", v.Description);
      }
   }
}