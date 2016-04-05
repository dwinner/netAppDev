using Microsoft.WindowsAPICodePack.ApplicationServices;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace EditorDemo
{
   public partial class MainWindow
   {
      private readonly CurrentFile _currentFile = new CurrentFile();
      private readonly DispatcherTimer _minuteTimer;
      private string _tempPath;

      public MainWindow()
      {
         InitializeComponent();
         var currentApp = Application.Current as App;
         if (currentApp != null && currentApp.RestartPath != null)
         {
            _tempPath = currentApp.RestartPath;
            _currentFile.Load(_tempPath);
            App.Source.TraceEvent(TraceEventType.Verbose, 0,
               string.Format("Application recovered data from the file {0}", _tempPath));
         }
         else
         {
            var tempFileName = string.Format("{0}.txt", Guid.NewGuid());
            _tempPath = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), tempFileName);
            App.Source.TraceEvent(
               TraceEventType.Verbose, 0, string.Format("Normal start with temp - filename {0}", _tempPath));
         }

         EditTextBox.DataContext = _currentFile;
         _minuteTimer = new DispatcherTimer(TimeSpan.FromSeconds(60), DispatcherPriority.Normal, On60Seconds,
            Dispatcher.CurrentDispatcher);

         RegisterForRestart();
         RegisterForRecovery();
      }

      private void RegisterForRecovery()
      {
         var settings = new RecoverySettings(new RecoveryData(DoRecovery, _tempPath), 0);
         ApplicationRestartRecoveryManager.RegisterForApplicationRecovery(settings);
         TraceTextBlock.Text += string.Format("{0:T}: Registered for recovery\n", DateTime.Now);
         App.Source.TraceEvent(TraceEventType.Verbose, 0, "Registered for recovery");
         // NOTE: Зарегистрировано для восстановления
      }

      private int DoRecovery(object state)
      {
         App.Source.TraceEvent(TraceEventType.Verbose, 0, "Begin recovery");
         _tempPath = state as string;
         var canceled = ApplicationRestartRecoveryManager.ApplicationRecoveryInProgress();
         if (canceled)
         {
            TraceTextBlock.Text += string.Format("{0:T}: Recovery canceled, shutting down\n", DateTime.Now);
            // NOTE: Восстановление отменено, завершение работы
            App.Source.TraceEvent(TraceEventType.Verbose, 0, "End recovery with cancel");
            ApplicationRestartRecoveryManager.ApplicationRecoveryFinished(false);
            return 0;
         }

         SaveFile(_tempPath);
         App.Source.TraceEvent(TraceEventType.Verbose, 0, "End recovery");
         ApplicationRestartRecoveryManager.ApplicationRecoveryFinished(true);

         return 0;
      }

      private static void SaveFile(string path) // TODO: Сохраняем файл на диске, в случае непредвиденного перезапуска
      {
         App.Source.TraceEvent(TraceEventType.Verbose, 0, string.Format("Saving file {0}", path));
      }

      private void RegisterForRestart()
      {
         var settings = new RestartSettings(string.Format("/restart:{0}", _tempPath),
            RestartRestrictions.NotOnReboot | RestartRestrictions.NotOnPatch);
         ApplicationRestartRecoveryManager.RegisterForApplicationRestart(settings);
         TraceTextBlock.Text += string.Format("{0:T}: Registered for restart\n", DateTime.Now);
         App.Source.TraceEvent(TraceEventType.Verbose, 0, "Registered for restart");
         // NOTE: Зарегистрировано для перезапуска 
      }

      private void On60Seconds(object sender, EventArgs e)
      {
         TraceTextBlock.Text += string.Format("{0:T}: application can crash now\n", DateTime.Now);
         _minuteTimer.Stop();
      }

      private void EditTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
      {
         if (!_currentFile.IsDirty)
         {
            _currentFile.IsDirty = true;
            TraceTextBlock.Text += string.Format("{0:T}: Set IsDirty\n", DateTime.Now);
         }
      }

      private void CrashMenuItem_OnClick(object sender, RoutedEventArgs e)
      {
         Environment.FailFast("Restart demo stopped from menu command");
      }
   }
}