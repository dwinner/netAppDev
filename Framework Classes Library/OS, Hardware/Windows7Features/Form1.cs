using System;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.ApplicationServices;
using Microsoft.WindowsAPICodePack.Shell;

namespace Windows7Features
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void buttonStdOFD_Click(object sender, EventArgs e)
      {
         var fileDialog = new OpenFileDialog();
         fileDialog.ShowDialog();
      }

      private void buttonWin7OFD_Click(object sender, EventArgs e)
      {
         var commonOpenFileDialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog
         {
            AddToMostRecentlyUsedList = true,
            IsFolderPicker = true,
            AllowNonFileSystemItems = true
         };
         // Позволяет обращаться к панели управления и библиотекам

         if (commonOpenFileDialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
         {
            ShellObject shellObj = commonOpenFileDialog.FileAsShellObject;
            var library = shellObj as ShellLibrary;
            if (library != null)
            {
               textBoxInfo.AppendText(string.Format("You picked a library: {0}, Type: {1}", library.Name, library.LibraryType));
               foreach (ShellFileSystemFolder folder in (ShellLibrary)shellObj)
               {
                  textBoxInfo.AppendText("\t" + folder.Path);
               }
            }
            textBoxInfo.AppendText(Environment.NewLine);
         }
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         GetPowerInfo();
      }

      private void GetPowerInfo()
      {
         labelPowerPersonality.Text = PowerManager.PowerPersonality.ToString();
         checkBoxBatteryPresent.Checked = PowerManager.IsBatteryPresent;
         checkBoxMonitorOn.Checked = PowerManager.IsMonitorOn;
         checkBoxUpsPresent.Checked = PowerManager.IsUpsPresent;
         labelPowerSource.Text = PowerManager.PowerSource.ToString();
      }
   }
}
