/**
 * The generic factories
 */

using System;

namespace GenericFactory
{
   internal static class Program
   {
      private static void Main()
      {
         var concreteProduct = ProductFactory.Create<ConcreteProduct>();
         Console.WriteLine(concreteProduct);
      }
   }
}