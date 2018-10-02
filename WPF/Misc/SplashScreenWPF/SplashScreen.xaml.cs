using System.Threading;

namespace SplashScreenWPF
{
   public partial class SplashScreen
   {
      private readonly UpdateStatusDelegate _updateDelegate;

      public SplashScreen()
      {
         InitializeComponent();
         _updateDelegate = UpdateStatus;
      }

      public void UpdateStatus(string status, int percent)
      {
         if (Dispatcher.Thread.ManagedThreadId != Thread.CurrentThread.ManagedThreadId)
         {
            Dispatcher.Invoke(_updateDelegate, status, percent);
         }
         else
         {
            StatusLabel.Content = status;
            ProgressBar.Value = percent;
         }
      }

      private delegate void UpdateStatusDelegate(string status, int percent);
   }
}