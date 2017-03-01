using System;
using System.Windows.Forms;
using SimpleVsWinFormsApp.Properties;

namespace SimpleVsWinFormsApp
{
   public partial class MainForm : Form
   {
      private string _lifeTimeInfo = string.Empty;

      public MainForm()
      {
         InitializeComponent();
      }

      private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
      {
          Close();   // Application.Exit();
      }

      private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         _lifeTimeInfo += string.Format("Form closing{0}", Environment.NewLine);
         var dialogResult = MessageBox.Show(Resources.FormClosingAlert, Resources.AlertTitle, MessageBoxButtons.YesNo);
         e.Cancel = dialogResult==DialogResult.No;
      }

      private void MainForm_Load(object sender, System.EventArgs e)
      {
         _lifeTimeInfo += string.Format("Load event{0}", Environment.NewLine);
      }

      private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
      {
         _lifeTimeInfo += string.Format("Form closed{0}", Environment.NewLine);
         MessageBox.Show(_lifeTimeInfo);
      }

      private void MainForm_Activated(object sender, System.EventArgs e)
      {
         _lifeTimeInfo += string.Format("Activate event{0}", Environment.NewLine);
      }

      private void MainForm_Deactivate(object sender, System.EventArgs e)
      {
         _lifeTimeInfo += string.Format("Deactivate event{0}", Environment.NewLine);
      }
   }
}