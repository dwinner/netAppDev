/**
 * Фабричные методы
 */

using System;

namespace FactoryMethod
{
   internal static class Program
   {
      private static void Main()
      {
         PageImpl catalogPage = new CatalogPage();
         catalogPage.AddModule();
         catalogPage.DisplayPage();

         PageImpl manualPage = new ManualPage();
         manualPage.AddModule();
         manualPage.DisplayPage();

         Console.ReadKey();
      }
   }
}