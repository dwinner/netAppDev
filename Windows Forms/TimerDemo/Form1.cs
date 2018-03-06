using System;
using System.Windows.Forms;

namespace TimerDemo
{
   public partial class Form1 : Form
   {
      private readonly Timer _timer;
      private bool _stopped = true;
      private bool _tick = true;

      public Form1()
      {
         InitializeComponent();

         _timer = new Timer { Interval = 1000 };         
         _timer.Tick += _timer_Tick;
      }

      private void _timer_Tick(object sender, EventArgs e)
      {
         labelOutput.Text = _tick ? "Tick" : "Tock";
         _tick = !_tick;
      }

      private void buttonStart_Click(object sender, EventArgs e)
      {
         if (_stopped)
         {
            _timer.Enabled = true;
            buttonStart.Text = "&Stop";
         }
         else
         {
            _timer.Enabled = false;
            buttonStart.Text = "&Start";
         }
         _stopped = !_stopped;
      }
   }
}