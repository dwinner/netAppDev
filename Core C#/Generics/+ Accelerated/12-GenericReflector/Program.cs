/**
 * Динамическое создание конструируемых типов
 * 
 * Note: механизм анализа некоторого языка, основанного на XML,
 * в котором определяются новые типы на основе обобщений становится очень простым
 */

using System;
using System.Collections.Generic;

namespace _12_GenericReflector
{
   class Program
   {
      static void Main()
      {
         var intList = (IList<int>)CreateClosedType<int>(typeof(List<>));
         var doubleList = (IList<double>)CreateClosedType<double>(typeof(List<>));

         Console.WriteLine(intList.GetType().FullName);
         Console.WriteLine(doubleList.GetType().FullName);

         Console.ReadKey();
      }

      static object CreateClosedType<T>(Type genericType)
      {
         Type[] typeArguments = { typeof(T) };
         Type closedType = genericType.MakeGenericType(typeArguments);
         return Activator.CreateInstance(closedType);
      }
   }
}
