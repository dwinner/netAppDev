using System.Runtime.InteropServices;

namespace SimpleHttpServer;

internal static class Program
{
   private const string UriPrefix = "http://localhost:51111/";

   private static void Main()
   {
      // Listen on port 51111, serving files in d:\webroot:
      var server = new WebServer(UriPrefix, Path.Combine(GetTempDirectory(), "webroot"));
      try
      {
         server.Start();
         Console.WriteLine("Server running... press Enter to stop");
         Console.ReadLine();
      }
      finally
      {
         server.Stop();
      }
   }

   private static string GetTempDirectory() =>
      RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"D:\Temp" : "/tmp";
}