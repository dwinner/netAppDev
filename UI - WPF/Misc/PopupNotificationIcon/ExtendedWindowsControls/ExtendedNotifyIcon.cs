using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedWindowsControls
{
   public sealed class ExtendedNotifyIcon : IDisposable
   {
      public delegate void MouseLeaveHandler();

      public delegate void MouseMoveHandler();

      private const int DefaultDelay = 100;

      private readonly Timer _delayMouseLeaveEventTimer;
      public readonly NotifyIcon TargetNotifyIcon;
      private Point _notifyIconMousePosition;

      public ExtendedNotifyIcon(int delay = DefaultDelay)
      {         
         TargetNotifyIcon = new NotifyIcon {Visible = true};
         TargetNotifyIcon.MouseMove += OnTargetNotifyIconMouseMove;

         _delayMouseLeaveEventTimer = new Timer();
         _delayMouseLeaveEventTimer.Tick += OnDelayMouseLeaveEventTimerTick;
         _delayMouseLeaveEventTimer.Interval = delay;
      }

      public event MouseLeaveHandler MouseLeave;
      public event MouseMoveHandler MouseMove;

      public void StopMouseLeaveEventFromFiring()
      {
         _delayMouseLeaveEventTimer.Stop();
      }

      private void OnTargetNotifyIconMouseMove(object sender, MouseEventArgs e)
      {
         _notifyIconMousePosition = Control.MousePosition;
         if (MouseMove != null)
            MouseMove.Invoke();         
         _delayMouseLeaveEventTimer.Start();
      }

      private void OnDelayMouseLeaveEventTimerTick(object sender, EventArgs e)
      {
         if (_notifyIconMousePosition != Control.MousePosition)
         {
            if (MouseLeave != null)
               MouseLeave.Invoke();
            _delayMouseLeaveEventTimer.Stop();
         }
      }

      #region IDisposable

      private bool _isDisposed;

      ~ExtendedNotifyIcon()
      {
         Dispose(false);
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      private void Dispose(bool isDisposing)
      {
         if (_isDisposed)
            return;

         if (isDisposing)
            TargetNotifyIcon.Dispose();

         _isDisposed = true;
      }

      #endregion
   }
}