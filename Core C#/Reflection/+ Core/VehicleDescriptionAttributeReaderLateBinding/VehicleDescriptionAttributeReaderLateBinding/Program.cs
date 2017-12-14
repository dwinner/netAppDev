using System;
using System.Reflection;

namespace VehicleDescriptionAttributeReaderLateBinding
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** Value of VehicleDescriptionAttribute *****\n");
         ReflectAttributesUsingLateBinding();
         Console.ReadLine();
      }

      private static void ReflectAttributesUsingLateBinding()
      {
         try
         {
            // Загрузка локальной копии сборки AttributedCarLibrary.
            var asm = Assembly.Load("AttributedCarLibrary");

            // Получение информации о типе VehicleDescriptionAttribute.
            var vehicleDesc = asm.GetType("AttributedCarLibrary.VehicleDescriptionAttribute");

            // Получение информации о типе Description.
            var propDesc = vehicleDesc.GetProperty("Description");

            // Получение всех типов из сборки.
            var types = asm.GetTypes();

            // Проход по типам с получением атрибутов VehicleDescriptionAttribute.
            foreach (var t in types)
            {
               var objs = t.GetCustomAttributes(vehicleDesc, false);

               // Проход по атрибутам VehicleDescriptionAttribute и вывод
               // описаний с использованием позднего связывания.
               foreach (var o in objs)
                  Console.WriteLine("-> {0}: {1}\n", t.Name, propDesc.GetValue(o, null));
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }
   }
}