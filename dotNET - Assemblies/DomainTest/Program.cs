/**
 * Загрузка сборок в другой домен приложения
 */

using System;
using System.Reflection;

namespace DomainTest
{
   class Program
   {
      static void Main()
      {
         AppDomain currentDomain = AppDomain.CurrentDomain;
         Console.WriteLine(currentDomain.FriendlyName);
         AppDomain secondDomain = AppDomain.CreateDomain("New Appdomain");
         //secondDomain.ExecuteAssembly("AssemblyA.exe");
         secondDomain.CreateInstance("AssemblyA", "AssemblyA.Demo", true, BindingFlags.CreateInstance, null,
            new object[] { 7, 3 }, null, null);
      }
   }
}
