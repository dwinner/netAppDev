// Нахождение интерфейсов

using System;
using static System.Console;

namespace FindingInterfaces
{
   internal static class Program
   {
      private static void Main()
      {
         var retailerType = typeof(MyRetailer);
         var interfaces = retailerType.FindInterfaces(TypeFilter, typeof(Program).Assembly);
         WriteLine("MyRetailer implements the following interfaces (defined in this assembly):");

         // Выводим сведения и каждом интерфейсе
         foreach (var iface in interfaces)
         {
            WriteLine("\nInterface: {0}", iface);

            // Получаем методы типа, соответствующие методам  интерфейса
            var interfaceMapping = retailerType.GetInterfaceMap(iface);
            for (var m = 0; m < interfaceMapping.InterfaceMethods.Length; m++)
            {
               WriteLine(" {0} is implemented by {1}", interfaceMapping.InterfaceMethods[m],
                  interfaceMapping.TargetMethods[m]);
            }
         }
      }

      /// <summary>
      ///    Критерий фильтрации
      /// </summary>
      /// <param name="aType">Тип</param>
      /// <param name="aFilterCriteria">Критерий</param>
      /// <returns>true, если интерфейс определен в сборке, определенной в <paramref name="aFilterCriteria" /></returns>
      private static bool TypeFilter(Type aType, object aFilterCriteria)
         => ReferenceEquals(aType.Assembly, aFilterCriteria);
   }

   internal interface IBookRetailer : IDisposable
   {
      void Purchase();
      void ApplyDiscount();
   }

   internal interface IMusicRetailer
   {
      void Purchase();
   }

   internal sealed class MyRetailer : IBookRetailer, IMusicRetailer
   {
      public void Dispose()
      {
      }

      void IBookRetailer.Purchase()
      {
      }

      public void ApplyDiscount()
      {
      }

      void IMusicRetailer.Purchase()
      {
      }
   }
}