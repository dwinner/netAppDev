/**
 * Использование специального поставщика ресурсов
 */

using System;
using System.Globalization;
using System.Resources;
using DatabaseResource.App.Lib;

namespace DatabaseResource.App
{
   internal static class Program
   {
      private const string ConnectionString =
         @"Data Source=(localdb)\ProjectsV12;Initial Catalog=Db_i18n;Integrated Security=True;Pooling=False;Connect Timeout=30";

      private static void Main()
      {
         ResourceManager resourceManager = new DatabaseResourceManager(ConnectionString);
         var spanishWelcome = resourceManager.GetString("Welcome", new CultureInfo("en-US"));
         var italianWelcome = resourceManager.GetString("ThankYou", new CultureInfo("it"));
         var threadDefaultGoodMorning = resourceManager.GetString("GoodMorning");

         Console.WriteLine(spanishWelcome);
         Console.WriteLine(italianWelcome);
         Console.WriteLine(threadDefaultGoodMorning);
      }
   }
}