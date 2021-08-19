using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace BackgroundApplication
{
   public partial class App
   {
      private bool _isExit;
      private NotifyIcon _notifyIcon;

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);
         MainWindow = new MainWindow();
         MainWindow.Closing += OnClosing;

         _notifyIcon = new NotifyIcon();
         _notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
         _notifyIcon.Icon = BackgroundApplication.Properties.Resources.MyIcon;
         _notifyIcon.Visible = true;

         CreateContextMenu();
      }

      private void CreateContextMenu()
      {
         _notifyIcon.ContextMenuStrip = new ContextMenuStrip();
         _notifyIcon.ContextMenuStrip.Items.Add("MainWindow...").Click += (s, e) => ShowMainWindow();
         _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
      }

      private void ExitApplication()
      {
         _isExit = true;
         MainWindow.Close();
         _notifyIcon.Dispose();
         _notifyIcon = null;
      }

      private void ShowMainWindow()
      {
         if (MainWindow.IsVisible)
         {
            if (MainWindow.WindowState == WindowState.Minimized)
               MainWindow.WindowState = WindowState.Normal;
            MainWindow.Activate();
         }
         else
         {
            MainWindow.Show();
         }
      }

      private void OnClosing(object sender, CancelEventArgs e)
      {
         if (!_isExit)
         {
            e.Cancel = true;
            MainWindow.Hide(); // A hidden window can be shown again, a closed one not
         }
      }
   }
}