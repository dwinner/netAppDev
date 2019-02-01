using Microsoft.WindowsAPICodePack.Taskbar;
using MS.WindowsAPICodePack.Internal;
using System;
using System.Windows;

namespace PictureViewerDemo
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {

      protected override void OnStartup(StartupEventArgs e)
      {
         TaskbarManager.Instance.ApplicationId = "PictureViewerDemo";
         base.OnStartup(e);
      }

      private void Application_Startup(object sender, StartupEventArgs e)
      {
         // If application not running on win 7 we exit.
         if (!CoreHelpers.RunningOnWin7)
         {
            MessageBox.Show("You are not running in Windows 7. You need windows 7 to run this application", "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Environment.Exit(0);
         }
      }

   }
}
