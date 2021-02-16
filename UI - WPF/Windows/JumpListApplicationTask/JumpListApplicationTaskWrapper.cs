using Microsoft.VisualBasic.ApplicationServices;

namespace JumpListApplicationTask
{
   public sealed class JumpListApplicationTaskWrapper :
      WindowsFormsApplicationBase
   {
      // Create the WPF application class.
      private WpfApp _app;

      public JumpListApplicationTaskWrapper()
      {
         // Enable single-instance mode.
         IsSingleInstance = true;
      }

      protected override bool OnStartup(StartupEventArgs e)
      {
         _app = new WpfApp();
         _app.Run();

         return false;
      }

      // Direct multiple instances
      protected override void OnStartupNextInstance(StartupNextInstanceEventArgs e)
      {
         if (e.CommandLine.Count > 0)
         {
            WpfApp.ProcessMessage(e.CommandLine[0]);
         }
      }
   }
}