/**
 * Загрузка и сохранение
 */

using System;
using System.Xml.Linq;

namespace SaveAndLoadXDocument
{
   internal static class Program
   {
      private static void Main()
      {
         var xDocument = XDocument.Load("hamlet.xml");
         var rootElement = xDocument.Root;
         if (rootElement != null)
         {
            Console.WriteLine(rootElement.Name.ToString());
            Console.WriteLine(rootElement.HasAttributes.ToString());
            xDocument.Save("CopyOfHamlet.xml");
         }
      }
   }
}