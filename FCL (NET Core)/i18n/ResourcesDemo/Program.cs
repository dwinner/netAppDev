using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace ResourcesDemo
{
   internal class Program
   {
      private static void Main()
      {
         var assembly = typeof(Program).GetTypeInfo().Assembly;
         //var type = typeof(Program).GetType();
         ResourceManager resources = new("ResourcesDemo.Resources.Messages", assembly);
         var goodMorning = resources.GetString("GoodMorning", new CultureInfo("de-CH"));
         Console.WriteLine(goodMorning);

         ResourceManager programResources = new(typeof(Program));
         Console.WriteLine(programResources.GetString("Resource1"));
      }
   }
}