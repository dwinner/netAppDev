/**
 * - Динамическое создание экземпляра класса,
 * - Димамически вызываемые методы
 */

using System;
using System.Reflection;

namespace DynamicInstantiate
{
   internal static class Program
   {
      private static void Main()
      {
         var assembly = Assembly.LoadFrom("DynamicInstantiateLib.dll");
         var type = assembly.GetType("DynamicInstantiate.TestClass");
         var obj = Activator.CreateInstance(type);

         // Вызов метода Add
         var result =
            (int)
            type.InvokeMember("Add", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null,
               obj, new object[] {1, 2});
         Console.WriteLine("result: {0}", result);

         // Вызов метода Add с помощью конструкции dynamic
         dynamic testClass = Activator.CreateInstance(type);
         result = testClass.Add(5, 6);
         Console.WriteLine("result: {0}", result);

         //invoke the generic CombineStrings<T> method using methodInfo
         var method = type.GetMethod("CombineStrings");
         var genericMethod = method?.MakeGenericMethod(typeof(double));
         var combined = (string) genericMethod?.Invoke(obj, new object[] {2.5, 5.5});
         Console.WriteLine("combined: {0}", combined);

         // Вызов CombineStrings<T> с помощью конструкции dynamic
         combined = testClass.CombineStrings<double>(13.3, 14.4);
         Console.WriteLine("combined: {0}", combined);

         Console.ReadKey();
      }
   }
}