using System.IO;
using System.Windows;

namespace LoadFromCommandLine
{
   public partial class App
   {
      // The command-line argument is set through the Visual Studio
      // project properties (the Debug tab).
      private void OnApplicationStartup(object sender, StartupEventArgs e)
      {
         // At this point, the main window has been created but not shown.
         var win = new FileViewer();

         if (e.Args.Length > 0)
         {
            var file = e.Args[0];
            if (File.Exists(file))
            {
               // Configure the main window.                    
               win.LoadFile(file);
            }
         }

         // This window will automatically be set as the Application.MainWindow.
         win.Show();
      }
   }
}