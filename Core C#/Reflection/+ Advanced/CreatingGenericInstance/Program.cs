// Создание экземпляров обобщенных типов

using System;
using System.Collections.Generic;
using static System.Console;

namespace CreatingGenericInstance
{
   internal static class Program
   {
      private static void Main()
      {
         // Получаем ссылку на объект Type обощенного типа
         var openType = typeof (Dictionary<,>);

         // Закрываем обобщенный тип, используя TKey=String, TValue=Int32
         var closedType = openType.MakeGenericType(typeof (string), typeof (int));

         // Создаем экземпляр закрытого типа
         var instance = Activator.CreateInstance(closedType);

         // Проверяем, работает ли наше решение
         WriteLine(instance.GetType());
      }
   }
}