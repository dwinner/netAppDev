using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HorizTiltWheelDemo
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
         panel1.MouseHWheel += panel1_MouseHWheel;
      }

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

      private void panel1_MouseHWheel(object sender, MouseEventArgs e)
      {
         label1.Text = string.Format("H Delta: {0}", e.Delta);
      }

      protected override void WndProc(ref Message m)
      {
         //send all mouse wheel messages to panel
         switch (m.Msg)
         {
            case Win32Messages.WmMousewheel:
            case Win32Messages.WmMousehwheel:
               SendMessage(panel1.Handle, m.Msg, m.WParam, m.LParam);
               m.Result = IntPtr.Zero;
               break;
         }

         base.WndProc(ref m);
      }
   }
}