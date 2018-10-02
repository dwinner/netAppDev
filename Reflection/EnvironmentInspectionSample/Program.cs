using System;
using System.Reflection;

namespace EnvironmentInspectionSample
{
   internal static class Program
   {
      private static void Main()
      {
         foreach (var field in typeof(Environment).GetProperties(BindingFlags.Public | BindingFlags.Static))
         {
            var fieldName = field.Name;
            var fieldValue = field.GetValue(null);
            Console.WriteLine($"{fieldName}: {fieldValue}");
         }
      }
   }
}