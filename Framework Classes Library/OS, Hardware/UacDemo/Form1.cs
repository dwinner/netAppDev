using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using EventLogDemo.Properties;

namespace EventLogDemo
{
   public partial class Form1 : Form
   {
      private EventLogEntryType _selectedLogEntryType = EventLogEntryType.Information;

      public Form1()
      {
         InitializeComponent();

         radioButtonInfo.Tag = EventLogEntryType.Information;
         radioButtonWarn.Tag = EventLogEntryType.Warning;
         radioButtonError.Tag = EventLogEntryType.Error;
      }

      private void buttonLogIt_Click(object sender, EventArgs e)
      {
         using (var log = new EventLog(Program.LogName, ".", Program.LogSource))
         {
            log.WriteEntry(textBoxMessage.Text, _selectedLogEntryType, (int)numericUpDown1.Value);
         }
      }

      private void CreateSource_Click(object sender, EventArgs e)
      {
         // NOTE: Повысить права у текущего процесса невозможно, придется запускать новый
         var startInfo = new ProcessStartInfo
         {
            FileName = Application.ExecutablePath,
            Arguments = "-createEventSource",
            Verb = "runas"
         };
         try
         {
            Process proc = Process.Start(startInfo);
            proc.WaitForExit();
         }
         catch (Exception ex)
         {
            MessageBox.Show(Resources.ProcessStartFailed + ex.Message);
         }
      }

      private void OnEntryTypeChanged(object sender, EventArgs e)
      {
         _selectedLogEntryType = (EventLogEntryType)(((RadioButton)sender).Tag);
      }

      private void buttonViewEvents_Click(object sender, EventArgs e)
      {
         using (var log = new EventLog(Program.LogName, ".", Program.LogSource))
         {
            var strBuffer = new StringBuilder();
            foreach (EventLogEntry entry in log.Entries)
            {
               strBuffer.AppendFormat("({0}, {1} {2}) {3}", entry.TimeGenerated, entry.InstanceId, entry.EntryType,
                  entry.Message);
               strBuffer.AppendLine();
            }
            MessageBox.Show(strBuffer.ToString(), "Existing events");
         }
      }
   }
}
