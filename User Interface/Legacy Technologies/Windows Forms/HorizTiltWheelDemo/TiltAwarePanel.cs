using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HorizTiltWheelDemo
{
   internal class TiltAwarePanel : Panel
   {
      //scrolling is in terms of lines and characters, which is app-defined
      private const int CharacterWidth = 8;
      private readonly uint _hScrollChars = 1;
      private int _wheelHPos;

      public TiltAwarePanel()
      {
         //get the system's values for horizontal scrolling
         if (!SystemParametersInfo(Win32Constants.SpiGetwheelscrollchars, 0, ref _hScrollChars, 0))
         {
            throw new InvalidOperationException("Unsupported on this platform");
         }
      }

      //FYI: There is a .Net SystemParameters class in WPF, but not in Winforms
      [DllImport("user32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref uint pvParam, uint fWinIni);

      public event EventHandler<MouseEventArgs> MouseHWheel;

      private void OnMouseHWheel(MouseEventArgs e)
      {
         //we have to accumulate the value
         _wheelHPos += e.Delta;
         //this method
         while (_wheelHPos >= Win32Constants.WheelDelta)
         {
            ScrollHorizontal((int) _hScrollChars*CharacterWidth);
            _wheelHPos -= Win32Constants.WheelDelta;
         }

         while (_wheelHPos <= -Win32Constants.WheelDelta)
         {
            ScrollHorizontal(-(int) _hScrollChars*CharacterWidth);
            _wheelHPos += Win32Constants.WheelDelta;
         }

         if (MouseHWheel != null)
         {
            MouseHWheel(this, e);
         }

         Refresh();
      }

      private void ScrollHorizontal(int delta)
      {
         AutoScrollPosition =
            new Point(
               -AutoScrollPosition.X + delta,
               -AutoScrollPosition.Y);
      }

      protected override void WndProc(ref Message m)
      {
         if (m.HWnd == Handle)
         {
            switch (m.Msg)
            {
               case Win32Messages.WmMousehwheel:
                  OnMouseHWheel(CreateMouseEventArgs(m.WParam, m.LParam));
                  //0 to indicate we handled it
                  m.Result = (IntPtr) 0;
                  return;
            }
         }
         base.WndProc(ref m);
      }

      private static MouseEventArgs CreateMouseEventArgs(IntPtr wParam, IntPtr lParam)
      {
         var buttonFlags = Loword(wParam);
         var buttons = MouseButtons.None;
         buttons |= ((buttonFlags & Win32Constants.MkLbutton) != 0) ? MouseButtons.Left : 0;
         buttons |= ((buttonFlags & Win32Constants.MkRbutton) != 0) ? MouseButtons.Right : 0;
         buttons |= ((buttonFlags & Win32Constants.MkMbutton) != 0) ? MouseButtons.Middle : 0;
         buttons |= ((buttonFlags & Win32Constants.MkXbutton1) != 0) ? MouseButtons.XButton1 : 0;
         buttons |= ((buttonFlags & Win32Constants.MkXbutton2) != 0) ? MouseButtons.XButton2 : 0;

         int delta = (Int16) Hiword(wParam);
         var x = Loword(lParam);
         var y = Hiword(lParam);

         return new MouseEventArgs(buttons, 0, x, y, delta);
      }

      private static Int32 Hiword(IntPtr ptr)
      {
         var val32 = ptr.ToInt32();
         return ((val32 >> 16) & 0xFFFF);
      }

      private static Int32 Loword(IntPtr ptr)
      {
         var val32 = ptr.ToInt32();
         return (val32 & 0xFFFF);
      }
   }
}