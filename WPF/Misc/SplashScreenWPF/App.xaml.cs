using System.Threading;
using System.Windows;

namespace SplashScreenWPF
{
   public partial class App
   {
      private static readonly SplashScreen _Splash = new SplashScreen();

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         var thread = new Thread((ThreadStart) delegate
         {
            //do loading tasks
            string[] fakeLoadingTasks =
            {
               "Loading greebles",
               "Refactoring image levels",
               "Doodling",
               "Adding dogs and cats",
               "Catmulling curves",
               "Taking longer just because"
            };

            for (var i = 0; i < fakeLoadingTasks.Length; i++)
            {
               if (_Splash != null)
                  _Splash.UpdateStatus(fakeLoadingTasks[i], 100 * i / fakeLoadingTasks.Length);
               Thread.Sleep(2000);
            }

            if (_Splash != null)
               _Splash.Dispatcher.Invoke((WpfMethodInvoker) delegate { _Splash.Close(); });
         });

         thread.Start();
         _Splash.Show();
      }

      private delegate void WpfMethodInvoker();
   }
}