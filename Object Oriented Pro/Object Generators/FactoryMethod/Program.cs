/**
 * Фабричные методы
 */

using System;

namespace FactoryMethod
{
   static class Program
   {
      static void Main()
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
