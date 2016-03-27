using System;
using System.Windows.Forms;

namespace TaskDialogDemo
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void btnClose_Click(object sender, EventArgs e)
      {
         Environment.Exit(2);
      }

      private void btnShow_Click(object sender, EventArgs e)
      {
         var taskdlg = new TaskDialog();

         var button = TaskDialogStandardButtons.Ok | TaskDialogStandardButtons.Cancel |
                      TaskDialogStandardButtons.Retry;

         if (rdError.Checked)
         {
            taskdlg.Icon = TaskDialogStandardIcon.Error;
         }
         else if (rdInfo.Checked)
         {
            taskdlg.Icon = TaskDialogStandardIcon.Information;
         }
         else if (rdShild.Checked)
         {
            taskdlg.Icon = TaskDialogStandardIcon.Shield;
         }
         else if (rdWarn.Checked)
         {
            taskdlg.Icon = TaskDialogStandardIcon.Warning;
         }

         taskdlg.Caption = "TaskDialogDemo Application Dialog";
         taskdlg.InstructionText = txtMessage.Text;
         taskdlg.Text = txtContent.Text;
         taskdlg.StandardButtons = button;
         taskdlg.DetailsExpandedLabel = "Please See the Content here ";
         taskdlg.DetailsExpandedText = txtCollapseText.Text;
         taskdlg.ExpansionMode = TaskDialogExpandedDetailsLocation.ExpandFooter;

         if (chkProgress.Checked)
         {
            taskdlg.ProgressBar = new TaskDialogProgressBar("Task ProgressBar")
            {
               Maximum = 100,
               Minimum = 0,
               State = TaskDialogProgressBarState.Normal,
               Value = 0
            };
            taskdlg.Tick += td_Tick;
         }
         var res = taskdlg.Show();

         MessageBox.Show(res.ToString());
      }

      private void td_Tick(object sender, TaskDialogTickEventArgs e)
      {
         var dlg = sender as TaskDialog;
         if (dlg.ProgressBar.Value == 100)
            dlg.ProgressBar.Value = 0;
         dlg.ProgressBar.Value += 1;
      }
   }
}