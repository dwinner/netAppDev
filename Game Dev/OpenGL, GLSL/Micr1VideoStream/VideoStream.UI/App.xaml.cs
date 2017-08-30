using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Palettes;
using Microsoft.Surface.Presentation.Input;

namespace VideoStream.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            SurfacePalette myPalette = new LightSurfacePalette();
            SurfaceColors.SetDefaultApplicationPalette(myPalette);
            Window window = window = new SurfaceShell();

            Application.Current.MainWindow = window;
            window.Show();
            window.Activate();
        }

        
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is System.Exception)
            {
                // HandleException((System.Exception)e.ExceptionObject, "UI Policy");
            }
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }
    }
}