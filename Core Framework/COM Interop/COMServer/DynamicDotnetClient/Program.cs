using System;

namespace DynamicDotnetClient
{
   internal static class Program
   {
      private static void Main()
      {
         var t = Type.GetTypeFromProgID("COMServer.COMDemo");
         dynamic o = Activator.CreateInstance(t);
         Console.WriteLine(o.Greeting("Angela"));
      }
   }
}