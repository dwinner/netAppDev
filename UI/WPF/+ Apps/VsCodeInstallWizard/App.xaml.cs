using System.Windows;

namespace MultiStudio.VsCodeInstallWizard
{
    public partial class App
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var startWindow = e.Args.Length == 1
                ? (Window)new VsCodeInstallExtensionsWindow(e.Args[0])
                : new VsCodeInstallWindow();

            startWindow.Show();
        }
    }
}