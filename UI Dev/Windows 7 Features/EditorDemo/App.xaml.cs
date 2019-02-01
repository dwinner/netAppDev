/**
 * Восстановление приложений
 */

using System;
using System.Diagnostics;
using System.Windows;

namespace EditorDemo
{
   public partial class App
   {
      public static readonly TraceSource Source = new TraceSource("RestartDemo");
      public string RestartPath { get; private set; }

      private void App_OnStartup(object sender, StartupEventArgs e)
      {
         Source.TraceEvent(TraceEventType.Verbose, 0, "Restart demo begin OnStartup");

         foreach (var arg in e.Args)
         {
            Source.TraceEvent(TraceEventType.Verbose, 0, string.Format("argument {0}", arg));

            if (arg.StartsWith("/restart", StringComparison.InvariantCultureIgnoreCase))
            {
               Source.TraceEvent(TraceEventType.Verbose, 0, "/restart: argument found");
               RestartPath = arg.Substring(9);
               Source.TraceEvent(TraceEventType.Verbose, 0, string.Format("RestartPath: {0}", RestartPath));
            }
         }
      }
   }
}