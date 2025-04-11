/*
 * Конфигурация сервера
 */

using System;
using System.Runtime.Remoting;

namespace Configuration.Server
{
   internal static class ConfigurationServer
   {
      private const string DefaultRmiConfig = "Configuration.Server.exe.config";

      private static void Main()
      {
         RemotingConfiguration.Configure(DefaultRmiConfig, false);
         Console.WriteLine("Application: {0}", RemotingConfiguration.ApplicationName);
         ShowActivatedServiceTypes();
         ShowWellKnownServiceTypes();
         Console.WriteLine("Press return to exit");
         Console.ReadLine();
      }

      private static void ShowWellKnownServiceTypes()
      {
         var serviceTypeEntries = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
         foreach (var typeEntry in serviceTypeEntries)
         {
            Console.WriteLine("Assembly: {0}", typeEntry.AssemblyName);
            Console.WriteLine("Mode: {0}", typeEntry.Mode);
            Console.WriteLine("URI: {0}", typeEntry.ObjectUri);
            Console.WriteLine("Type: {0}", typeEntry.TypeName);
         }
      }

      private static void ShowActivatedServiceTypes()
      {
         var entries = RemotingConfiguration.GetRegisteredActivatedServiceTypes();
         foreach (var entry in entries)
         {
            Console.WriteLine("Assembly: {0}", entry.AssemblyName);
            Console.WriteLine("Type: {0}", entry.TypeName);
         }
      }
   }
}