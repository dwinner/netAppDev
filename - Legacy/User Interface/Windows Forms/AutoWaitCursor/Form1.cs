using System;
using System.Threading;
using System.Windows.Forms;

namespace AutoWaitCursor
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
         Application.ThreadException += Application_ThreadException;
      }

      private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
      {
         MessageBox.Show("Unhandled exception (which I'm handling)");
      }

      private void buttonAutoWait_Click(object sender, EventArgs e)
      {
         using (new AutoWaitCursor(this))
         {
            throw new Exception("Ooops, something happened!");
         }
      }

      private void buttonOldWait_Click(object sender, EventArgs e)
      {
         Cursor = Cursors.WaitCursor;
         throw new Exception("Ooops, something happened!");
      }

      private void buttonResetCursor_Click(object sender, EventArgs e)
      {
         Cursor = Cursors.Default;
      }
   }
}