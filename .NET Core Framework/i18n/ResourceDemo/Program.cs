/**
 * Использование ресурсных файлов
 */

using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using ResourceDemo.Properties;

namespace ResourceDemo
{
   class Program
   {
      static void Main()
      {
         UsingExternalResources();
         StronglyTypedResources();
      }

      private static void UsingExternalResources() // Использование сторонних ресурсов
      {
         var resourceManager = new ResourceManager("ResourceDemo.Properties.Resources", Assembly.GetExecutingAssembly());
         Console.WriteLine(resourceManager.GetString("Title"));
         Console.WriteLine(resourceManager.GetString("Chapter"));
         Console.WriteLine(resourceManager.GetString("Author"));
         using (var logo = (Image)resourceManager.GetObject("Logo"))
         {
            if (logo != null)
               logo.Save("logo.bmp");
         }
      }

      private static void StronglyTypedResources() // Использование (встроенных) строго типизированных ресурсов
      {
         Console.WriteLine(Resources.Title);
         Console.WriteLine(Resources.Chapter);
         Console.WriteLine(Resources.Author);
         using (var logo = Resources.Logo)
         {
            if (logo != null)
               logo.Save("logo2.bmp");
         }
      }
   }
}
