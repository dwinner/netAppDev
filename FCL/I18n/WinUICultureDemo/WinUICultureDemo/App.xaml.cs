using Microsoft.UI.Xaml;

namespace WinUICultureDemo
{
   public partial class App
   {
      private Window? _mainWindow;

      public App()
      {
         InitializeComponent();
      }

      protected override void OnLaunched(LaunchActivatedEventArgs args)
      {
         _mainWindow = new MainWindow();
         _mainWindow.Activate();
      }
   }
}