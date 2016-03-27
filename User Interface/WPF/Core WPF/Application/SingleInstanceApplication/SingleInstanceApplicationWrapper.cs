using Microsoft.VisualBasic.ApplicationServices;

namespace SingleInstanceApplication
{
   public sealed class SingleInstanceApplicationWrapper : WindowsFormsApplicationBase
   {
      // Create the WPF application class.
      private WpfApplication _application;

      public SingleInstanceApplicationWrapper()
      {
         // Enable single-instance mode.
         IsSingleInstance = true;
      }

      protected override bool OnStartup(StartupEventArgs e)
      {
         const string extension = ".testDoc";
         const string title = "SingleInstanceApplication";
         const string extensionDescription = "A Test Document";         
         
         FileRegistrationHelper.SetFileAssociation(extension, string.Format("{0}.{1}", title, extensionDescription));
         _application = new WpfApplication();
         _application.Run();

         return false;
      }

      // Direct multiple instances
      protected override void OnStartupNextInstance(StartupNextInstanceEventArgs e)
      {
         if (e.CommandLine.Count > 0)
         {
            _application.ShowDocument(e.CommandLine[0]);
         }
      }
   }
}