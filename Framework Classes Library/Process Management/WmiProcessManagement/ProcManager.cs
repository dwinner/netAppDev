using System;
using System.Windows.Forms;
using WmiProcessManagement.Properties;

namespace WmiProcessManagement
{
   public partial class ProcManager : Form
   {
      public ProcManager()
      {
         InitializeComponent();

         var notePad = new ProcessInfo("notepad.exe");
         notePad.Started += OnNotepadStarted;
         notePad.Terminated += OnNotepadTerminated;
      }

      private void OnNotepadTerminated(object sender, EventArgs e)
      {
         notepadLabel.Text = Resources.NotepadStopped;
      }

      private void OnNotepadStarted(object sender, EventArgs e)
      {
         notepadLabel.Text = Resources.NotepadStarted;
      }

      private void OnGetProcessesClick(object sender, EventArgs e)
      {
         var processList = ProcessInfo.RunningProcesses();
         processGrid.DataSource = processList;
      }
   }
}