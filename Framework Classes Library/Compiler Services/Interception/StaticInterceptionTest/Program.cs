// Код тестирования атрибута

using System;

namespace StaticInterceptionTest
{
   internal static class Program
   {
      private static void Main() => MethodToChange("Test");

      [TestMethodInterception]
      private static void MethodToChange(string text) => Console.WriteLine(text);
   }
}