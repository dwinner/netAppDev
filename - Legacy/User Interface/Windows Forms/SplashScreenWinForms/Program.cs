/**
 * Создание экрана заставки
 */

using System;
using System.Threading;
using System.Windows.Forms;
using SplashScreenWinForms.Properties;

namespace SplashScreenWinForms
{
   internal static class Program
   {
      private static Thread _loadThread;
      private static SplashScreen _splash;
      
      [STAThread]
      private static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         _splash = new SplashScreen(Resources.splash);

         _loadThread = new Thread((ThreadStart) delegate
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
               if (_splash != null)
               {
                  _splash.UpdateStatus(fakeLoadingTasks[i], 100*i/fakeLoadingTasks.Length);
               }
               Thread.Sleep(2000);
            }
            Win32.PlaySound(".Default", 0,
               (int) (Win32.Soundflags.SndAlias | Win32.Soundflags.SndAsync | Win32.Soundflags.SndNowait));
            if (_splash != null)
               _splash.Invoke((MethodInvoker) delegate { _splash.Close(); });
         });
         _loadThread.Start();


         _splash.TopLevel = true;
         _splash.ShowDialog();

         Application.Run(new Form1());
      }
   }
}