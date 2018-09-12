using System;
using System.Threading;
using System.Windows.Forms;
using PsThreading.Properties;
using PostSharp.Patterns.Threading;

namespace PsThreading
{
   public partial class PsThreadingMain : Form
   {
      public PsThreadingMain()
      {
         InitializeComponent();
      }

      [Background]
      private void UpdateButton_Click(object sender, EventArgs e)
      {
         ThisProcessTakesALongTime();
         SignalFinished();
      }

      [Dispatched]
      private void SignalFinished() => UpdateTextBox.Text = Resources.PsThreadingMain_SignalFinished_Done;

      private void ThisProcessTakesALongTime() => Thread.Sleep(3000);
   }
}