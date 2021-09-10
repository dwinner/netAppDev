using System;
using System.Windows.Forms;

namespace DefineEvent
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();

         var data = new ProgramData();
         data.LoadStarted += Data_LoadStarted;
         data.LoadEnded += Data_LoadEnded;

         data.BeginLoad();
      }

      void Data_LoadStarted(object sender, EventArgs e)
      {
         if (InvokeRequired)
         {
            // Если это не поток пользовательского интерфейса, делать
            // рекурсивный вызов, пока управление не будет передано
            // потоку пользовательского интерфейса
            Invoke(new EventHandler<EventArgs>(Data_LoadStarted), sender, e);
         }
         else
         {
            textBoxLog.AppendText(
               string.Format("Load started{0}", Environment.NewLine));
         }
      }

      void Data_LoadEnded(object sender, ProgramDataEventArgs e)
      {
         if (InvokeRequired)
         {
            Invoke(new EventHandler<ProgramDataEventArgs>(Data_LoadEnded), sender, e);
         }
         else
         {
            textBoxLog.AppendText(
               string.Format("Load ended (elapsed: {0})" + Environment.NewLine, e.LoadTime));
         }
      }
   }
}
