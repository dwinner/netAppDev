using ProgramVerificationSystems.SelfTester.Model.Poco;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace ProgramVerificationSystems.SelfTester.UI
{
   /// <summary>
   /// Модель представления для таймера
   /// </summary>
   public sealed class TimerViewModel : ViewModelBase
   {
      private const string StartTimeElapsed = "00:00:00";

      public string TimeElapsed
      {
         get { return _timeElapsed; }
         set
         {
            _timeElapsed = value;
            OnPropertyChanged();
         }
      }

      private string _timeElapsed;
      private readonly Stopwatch _stopwatch;
      private readonly DispatcherTimer _timer;

      public TimerViewModel()
      {
         _timer = new DispatcherTimer();
         TimeElapsed = StartTimeElapsed;
         _timer.Tick += (sender, args) =>
         {
            string fmtElapsed = _stopwatch.Elapsed.ToString("t");
            int founded = fmtElapsed.IndexOf('.');
            if (founded > 0)
               TimeElapsed = fmtElapsed.Substring(0, founded);
         };
         _timer.Interval = new TimeSpan(0, 0, 0, 1);
         _stopwatch = new Stopwatch();
      }

      public void Start()
      {
         TimeElapsed = StartTimeElapsed;
         _stopwatch.Start();
         _timer.Start();
      }

      public void Stop()
      {
         _stopwatch.Reset();
         _timer.Stop();
      }
   }
}