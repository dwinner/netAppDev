using System.Windows;
using System.Windows.Threading;

namespace PreventSessionEnd
{
   public partial class SessionEndApp
   {
      public SessionEndApp()
      {
         UnsavedData = false;
      }

      public bool UnsavedData { get; set; }

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);
         UnsavedData = true;
      }

      protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
      {
         base.OnSessionEnding(e);

         if (UnsavedData)
         {
            e.Cancel = true;
            MessageBox.Show(
               string.Format(
                  "The application attempted to close as a result of {0}. This is not allowed, as you have unsaved data.",
                  e.ReasonSessionEnding));
         }
      }

      private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
      {
         MessageBox.Show(string.Format("An unhandled {0} exception was caught and ignored.", e.Exception.GetType()));
         e.Handled = true;
      }
   }
}