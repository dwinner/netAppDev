﻿using System;
using System.Runtime.Remoting;

namespace Events.Server
{
   internal static class EventServer
   {
      private static void Main()
      {
         RemotingConfiguration.Configure("Events.Server.exe.config", false);
         Console.WriteLine("Press return to exit");
         Console.ReadLine();
      }
   }
}