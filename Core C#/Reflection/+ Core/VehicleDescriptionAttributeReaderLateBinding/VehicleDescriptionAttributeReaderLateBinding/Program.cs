using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace VehicleDescriptionAttributeReaderLateBinding
{
   class Program
   {
      static void Main(string[] args)
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
            Assembly asm = Assembly.Load("AttributedCarLibrary");

            // Получение информации о типе VehicleDescriptionAttribute.
            Type vehicleDesc = asm.GetType("AttributedCarLibrary.VehicleDescriptionAttribute");

            // Получение информации о типе Description.
            PropertyInfo propDesc = vehicleDesc.GetProperty("Description");

            // Получение всех типов из сборки.
            Type[] types = asm.GetTypes();

            // Проход по типам с получением атрибутов VehicleDescriptionAttribute.
            foreach (Type t in types)
            {
               object[] objs = t.GetCustomAttributes(vehicleDesc, false);

               // Проход по атрибутам VehicleDescriptionAttribute и вывод
               // описаний с использованием позднего связывания.
               foreach (object o in objs)
               {
                  Console.WriteLine("-> {0}: {1}\n", t.Name, propDesc.GetValue(o, null));
               }
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }
   }
}
